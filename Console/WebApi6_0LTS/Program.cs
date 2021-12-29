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

var builder = WebApplication.CreateBuilder(args);
// Look for static files in webroot
//builder.WebHost.UseWebRoot("webroot");

//builder.WebHost.UseUrls("https://*:9080", "http://*:9081");

// Wait 30 seconds for graceful shutdown.
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

//var url = builder.Configuration[WebHostDefaults.ServerUrlsKey];

// Add services to the container.


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
    options.Filters.Add<TokenCheckFilter>(); 
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

builder.Services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>();

#region  Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));

#endregion Autofac

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

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

app.UseHttpsRedirection();

GlobalConfig.ServiceProvider = app.Services;

app.UseAuthorization();
//解决跨域
app.UseCors("CorsPolicy");

app.UseCalculateExecutionTime();

app.UseMiddleware(typeof(ExceptionMiddleWare));

app.UseStateAutoMapper();

app.UseShardResource();

app.UseHangfire();
 
app.MapControllers();

//app.Lifetime.ApplicationStarted.Register(ApplicationConfig.OnAppStarted);
//app.Lifetime.ApplicationStopped.Register(ApplicationConfig.OnAppStopped);
 
app.Run();

