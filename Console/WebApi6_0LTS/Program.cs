using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Framework.Hangfire;
using Framework.Log4Net;
using Framework.Mapper;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Email;
using Framework.Utility.JWT;
using Framework.Utility.Mapping;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;
using WebApi6_0.AppConfig;
using WebApi6_0.Extensions;
using WebApi6_0.Filter;
using WebApi6_0.HangFire;
using WebApi6_0.Middleware;

//using ZipDeploy;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

// Look for static files in webroot

//builder.WebHost.UseWebRoot("wwwroot");

//var configuration = new ConfigurationBuilder()
//                      .AddJsonFile("appsettings.json").Build();

var currentDir = Directory.GetCurrentDirectory();
builder.Host.UseContentRoot(currentDir);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    config.AddEnvironmentVariables();
});

//var dddd = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
//var ddd2 = builder.Configuration.GetSection("MailSender").Get<MailSenderOptions>();

//builder.WebHost.UseUrls("https://*:9080", "http://*:9081");

// Wait 30 seconds for graceful shutdown.
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(60));

// Add services to the container.

builder.Services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add<TokenCheckFilter>();
    options.Filters.Add<ApiResultFilterAttribute>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//忽略循环引用
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//序列化保持原有大小写（默认首字母小写）
});

builder.Services.AddDistributedMemoryCache();


//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//    options.CheckConsentNeeded = context => false; //这里要改为false，默认是true，true的时候session无效
//    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
//});

//分布式环境下设置相同的程序识别者
builder.Services.AddDataProtection(configure =>
{
    configure.ApplicationDiscriminator = "commonweb1";
}).SetApplicationName("commonweb1");

//.AddKeyManagementOptions(options =>
//{
//  //配置自定义XmlRepository
//  options.XmlRepository = new XmlRepository();
//});


#region 使用Redis保存Session
// 这里取连接字符串
builder.Services.AddDistributedRedisCache(option =>
{
    //redis 连接字符串
    option.Configuration = "127.0.0.1:6379";
    //redis 实例名
    option.InstanceName = "Test_Session";
});

// 注册Session服务
builder.Services.AddSession(opt => { 
    opt.IdleTimeout= TimeSpan.FromSeconds(60);//60秒
    opt.Cookie.HttpOnly = true;//设置httponly
});

#endregion

//解决跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApi6_0_测试api", Version = "v1" });
    // 配置从xml文档中获取描述信息
    // 路径我们获取的项目路径+startup命名空间（也可以直接写生成的xml名称）
    var filePath = Path.Combine(System.AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddAutoMapper(MapperRegister.MapType());

//builder.Services.AddHangfire(builder.Configuration);

//builder.Services.AddSingleton<IHangfireJobRunner, HangfireJobRunner>();

builder.Services.AddSingleton<IEmailSender, DefaultEmailSender>();

builder.Services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>(); //log4net
                                                                         //builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();//NLog
builder.Services.AddFeatureManagement();

#region Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));

#endregion Autofac

builder.Services.AddEndpointsApiExplorer();

JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
//配置鉴权流程
builder.Services.AddAuthenticationExtension(tokenOptions);

//RabbitMQOptions mqOptions = new RabbitMQOptions();
//builder.Configuration.Bind("RabbitMQOptions", mqOptions);
//builder.Services.AddRabbitMQ(option => option = mqOptions);//RabbitMQ

builder.Services.Configure<SystemConfig>(options =>
{
    builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
});

GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
builder.Configuration.Bind("MailSender", GlobalConfig.MailSenderOptions);

GlobalConfig.Services = builder.Services;
GlobalConfig.Configuration = builder.Configuration;

//builder.Services.AddZipDeploy();//发布压缩组件

var app = builder.Build();
//启用静态文件
app.UseStaticFiles();

////这一步很关键
//DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
//defaultFilesOptions.DefaultFileNames.Clear();
////设置首页，我希望用户打开`localhost`访问到的是`wwwroot`下的Index.html文件
//defaultFilesOptions.DefaultFileNames.Add("swg-login.html");
//app.UseDefaultFiles(defaultFilesOptions);

// 使用中间件，放到Swagger中间件之前
app.UseSession();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");//解决跨域

app.UseSwaggerAuthorized();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

#region 异常处理

//RequestDelegate handler = async context =>
//{
//    var exceptionHandlerPathFeature =
//    context.Features.Get<IExceptionHandlerPathFeature>();

//    var resp = new BaseResponse(successCode.Error, exceptionHandlerPathFeature?.Error.Message!);
//    var exception = exceptionHandlerPathFeature?.Error;

//    while (exception?.InnerException != null)
//    {
//        resp.msg = exception.InnerException.Message;
//        exception = exception.InnerException;
//    }
//    context.Response.ContentType = "application/json";
//    context.Response.StatusCode = 200;
//    await context.Response.WriteAsync(JsonConvert.SerializeObject(resp)).ConfigureAwait(false);
//};

//app.UseExceptionHandler(new ExceptionHandlerOptions
//{
//    ExceptionHandler = handler
//});

#endregion 异常处理

//app.UseCookiePolicy();

app.UseAuthorization();//鉴权
app.UseAuthentication();//授权

app.UseCalculateExecutionTime();

app.UseMiddleware(typeof(ExceptionMiddleWare));

app.UseMiddleware(typeof(DebugMiddleware));

app.UseStateAutoMapper();

app.UseShardResource();

//app.UseHangfire();
//app.UseRabbitMQ();//RabbitMQ

app.MapControllers();
GlobalConfig.ServiceProvider = app.Services;
app.Run();