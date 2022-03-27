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
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//����ѭ������
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//���л�����ԭ�д�Сд��Ĭ������ĸСд��
});

builder.Services.AddDistributedMemoryCache();


//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//    options.CheckConsentNeeded = context => false; //����Ҫ��Ϊfalse��Ĭ����true��true��ʱ��session��Ч
//    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
//});

//�ֲ�ʽ������������ͬ�ĳ���ʶ����
builder.Services.AddDataProtection(configure =>
{
    configure.ApplicationDiscriminator = "commonweb1";
}).SetApplicationName("commonweb1");

//.AddKeyManagementOptions(options =>
//{
//  //�����Զ���XmlRepository
//  options.XmlRepository = new XmlRepository();
//});


#region ʹ��Redis����Session
// ����ȡ�����ַ���
builder.Services.AddDistributedRedisCache(option =>
{
    //redis �����ַ���
    option.Configuration = "127.0.0.1:6379";
    //redis ʵ����
    option.InstanceName = "Test_Session";
});

// ע��Session����
builder.Services.AddSession(opt => { 
    opt.IdleTimeout= TimeSpan.FromSeconds(60);//60��
    opt.Cookie.HttpOnly = true;//����httponly
});

#endregion

//�������
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
    c.SwaggerDoc("v1", new() { Title = "WebApi6_0_����api", Version = "v1" });
    // ���ô�xml�ĵ��л�ȡ������Ϣ
    // ·�����ǻ�ȡ����Ŀ·��+startup�����ռ䣨Ҳ����ֱ��д���ɵ�xml���ƣ�
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
//���ü�Ȩ����
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

//builder.Services.AddZipDeploy();//����ѹ�����

var app = builder.Build();
//���þ�̬�ļ�
app.UseStaticFiles();

////��һ���ܹؼ�
//DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
//defaultFilesOptions.DefaultFileNames.Clear();
////������ҳ����ϣ���û���`localhost`���ʵ�����`wwwroot`�µ�Index.html�ļ�
//defaultFilesOptions.DefaultFileNames.Add("swg-login.html");
//app.UseDefaultFiles(defaultFilesOptions);

// ʹ���м�����ŵ�Swagger�м��֮ǰ
app.UseSession();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");//�������

app.UseSwaggerAuthorized();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

#region �쳣����

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

#endregion �쳣����

//app.UseCookiePolicy();

app.UseAuthorization();//��Ȩ
app.UseAuthentication();//��Ȩ

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