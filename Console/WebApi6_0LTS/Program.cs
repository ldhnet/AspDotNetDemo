using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Mapping;  
using WebApi6_0.Middleware;
using WebApi6_0.Filter;
using Newtonsoft.Json; 
using AutoMapper;
using Framework.Mapper;
using Framework.Hangfire;
using WebApi6_0.HangFire;
using WebApi6_0.AppConfig;
using Microsoft.AspNetCore.Diagnostics;
using Framework.Utility.Email;
using Framework.Log4Net;
using Framework.NLog;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Framework.Utility.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi6_0.Extensions;
using Framework.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
// Look for static files in webroot
//builder.WebHost.UseWebRoot("webroot");
 
//var configuration = new ConfigurationBuilder()
//                      .AddJsonFile("appsettings.json").Build();
 
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());
 
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
//解决跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
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

builder.Services.AddControllers(options => {
    //options.Filters.Add<TokenCheckFilter>(); 
    options.Filters.Add<ApiResultFilterAttribute>();
}).AddNewtonsoftJson(options=>
{
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//忽略循环引用
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//序列化保持原有大小写（默认首字母小写）
});
builder.Services.AddAutoMapper(MapperRegister.MapType());

builder.Services.AddHangfire(builder.Configuration);

builder.Services.AddSingleton<IHangfireJobRunner, HangfireJobRunner>();

builder.Services.AddSingleton<IEmailSender, DefaultEmailSender>();

builder.Services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>(); //log4net 
//builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();//NLog
 
#region  Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));

#endregion Autofac

builder.Services.AddEndpointsApiExplorer();



JWTTokenOptions tokenOptions=new JWTTokenOptions();
builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
//配置鉴权流程
builder.Services.AddAuthenticationExtension(tokenOptions);


//RabbitMQOptions mqOptions = new RabbitMQOptions();
//builder.Configuration.Bind("RabbitMQOptions", mqOptions);

//builder.Services.AddRabbitMQ(option => option = mqOptions);//RabbitMQ

//var url = builder.Configuration[WebHostDefaults.ServerUrlsKey];

builder.Services.Configure<SystemConfig>(options =>
{
    builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
});


GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

//GlobalConfig.MailSenderOptions = builder.Configuration.GetSection("MailSender").Get<MailSenderOptions>();

builder.Configuration.Bind("MailSender", GlobalConfig.MailSenderOptions);
 
GlobalConfig.Services = builder.Services;
GlobalConfig.Configuration = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

#region 异常处理
RequestDelegate handler = async context =>
{
    var exceptionHandlerPathFeature =
    context.Features.Get<IExceptionHandlerPathFeature>();

    var resp = new BaseResponse(successCode.Error, exceptionHandlerPathFeature?.Error.Message!);
    var exception = exceptionHandlerPathFeature?.Error;

    while (exception?.InnerException != null)
    {
        resp.msg = exception.InnerException.Message;
        exception = exception.InnerException;
    }
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync(JsonConvert.SerializeObject(resp)).ConfigureAwait(false);
};

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = handler
});

#endregion

app.UseHttpsRedirection();
 
app.UseAuthorization();//鉴权
app.UseAuthentication();//授权
//解决跨域
app.UseCors("CorsPolicy");

app.UseCalculateExecutionTime();

app.UseMiddleware(typeof(ExceptionMiddleWare));

app.UseStateAutoMapper();

app.UseShardResource();

app.UseHangfire();

//app.UseRabbitMQ();//RabbitMQ

app.MapControllers();

//app.Lifetime.ApplicationStarted.Register(ApplicationConfig.OnAppStarted);
//app.Lifetime.ApplicationStopped.Register(ApplicationConfig.OnAppStopped);

GlobalConfig.ServiceProvider = app.Services;
app.Run();

