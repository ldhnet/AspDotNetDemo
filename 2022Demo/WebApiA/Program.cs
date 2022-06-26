using Autofac;
using Autofac.Extensions.DependencyInjection;
using Lee.EF.Context;
using Lee.Hangfire;
using Lee.Repository;
using Lee.Repository.Data;
using Lee.Repository.Repository;
using Lee.Utility.Dependency;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Reflection;
using WebA.Admin.Contracts;
using WebA.Admin.Service;
using WebA.Constant;
using WebApiA.Attributes;
using WebApiA.Filter;
using WebApiA.Middleware;

//kestrel限制请求体最大为：28M；
//formbody最大为：128M

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:9010");
// Add services to the container.
builder.WebHost.UseWebRoot("wwwroot");
//var currentDir = Directory.GetCurrentDirectory();
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

//解除kestrel限制
builder.WebHost.ConfigureKestrel(options =>
{
    //options.Limits.MaxRequestBodySize = 30000000L;//默认约28M
    //options.Limits.MaxRequestBodySize = 2 * 2L << 30;//指定最大2G
    options.Limits.MaxRequestBodySize = null;//去掉限制 仅用于演示
});

//解除 formbody限制
builder.Services.Configure<FormOptions>(x =>
{
    //x.MultipartBodyLengthLimit = 134217728;//默认128MB
    x.MultipartBodyLengthLimit = 5 * 2L << 30;//这里手动设置为5GB,这么大的数值仅用于演示
}); 


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDataProtection();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region 使用Redis保存Session
// 使用SqlServer保存Session
//builder.Services.AddSqlServerCache(o =>
//{
//    o.ConnectionString = "Server=.;Database=test;Trusted_Connection=True;";
//    o.SchemaName = "dbo";
//    o.TableName = "Sessions";
//});


//// 使用Redis保存Session
//builder.Services.AddDistributedRedisCache(option =>
//{
//    //redis 连接字符串
//    option.Configuration = "127.0.0.1:6379";
//    //redis 实例名
//    option.InstanceName = "ApiA";
//});

// 注册Session服务
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Lee.Session";
    options.IdleTimeout = System.TimeSpan.FromSeconds(30 * 60);//设置session的过期时间 单位秒
    options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
});

#endregion

#region 跨域
builder.Services.AddCors(options => options.AddPolicy("AllowSameDomain",builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
#endregion

builder.Services.Configure<FormOptions>(options => {
    options.ValueLengthLimit = 209715200;//200MB   //.netcore 限制了每个 POST 数据值的长度为 4M  提升到200M
});

//builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApiA_测试api", Version = "v1" });
    // 配置从xml文档中获取描述信息
    // 路径我们获取的项目路径+startup命名空间（也可以直接写生成的xml名称）
    var filePath = Path.Combine(System.AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddDbContextPool<MyDBContext>(options =>
{
    var connection = "server=localhost;userid=root;pwd=2021@ldh;port=3306;database=dh;sslmode=none;Convert Zero Datetime=True";

    options.UseMySql(connection, ServerVersion.Create(8, 0, 29, ServerType.MySql));
     
}, 60);

//builder.Services.AddSingleton<MyDBContext>();
//builder.Services.AddSingleton<MyAdminContext>();
//builder.Services.AddSingleton<ServiceContext>();

//builder.Services.AddSingleton(typeof(IRepository<,>),typeof(Repository<,>));

//builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();


builder.Services.AddSingleton<ITestRepository, TestRepository>();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<IVisitRecordRepository, VisitRecordRepository>();

//builder.Services.AddSingleton<ISystemContract, SystemService>();

builder.Services.AddScoped<MyFilter>();


#region Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<MyDBContext>().AsSelf().InstancePerLifetimeScope();     
    builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
     
    //builder.RegisterType<ServiceContext>().AsSelf().InstancePerLifetimeScope();

    Type baseType = typeof(IDependency);      
    var assemblieTypes = Assembly.GetEntryAssembly().GetReferencedAssemblies()
    .Select(Assembly.Load)
    .SelectMany(c => c.GetExportedTypes())
    .Where(t => baseType.IsAssignableFrom(t))
    .ToArray();     
    builder.RegisterTypes(assemblieTypes).AsSelf().InstancePerLifetimeScope(); //保证生命周期基于请求 


    builder.Register<MyAdminContext>(context =>
    {
        var httpContextAccessor = context.Resolve<IHttpContextAccessor>();
        var httpContext = httpContextAccessor.HttpContext;

        var aaaaa = httpContext.Request.Headers.Keys;

        var bbbb = httpContext.Request.Headers["Host"];

        MyAdminContext _context = new MyAdminContext();
        _context.CurrentID = 1;
        _context.CurrentMonth = DateTime.Now;
        return _context;
    }).AsSelf().InstancePerLifetimeScope();


    var assemblies = Assembly.GetEntryAssembly()?//获取默认程序集
            .GetReferencedAssemblies()//获取所有引用程序集
            .Select(Assembly.Load)
            .Where(c => c.FullName!.Contains("WebA.Admin", StringComparison.OrdinalIgnoreCase))
            .ToArray();

    builder.RegisterAssemblyTypes(assemblies!)
        .Where(type => !type.IsAbstract)
        .AsSelf()   //自身服务，用于没有接口的类
        .AsImplementedInterfaces()  //接口服务
        .PropertiesAutowired()  //属性注入
        .InstancePerLifetimeScope(); //保证生命周期基于请求 

    //支持属性注入
    var controllerBaseType = typeof(ControllerBase);
    builder.RegisterAssemblyTypes(typeof(Program).Assembly)
    .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
    .PropertiesAutowired(new CustomPropertySelector());//支持属性注入
});

//支持容器的实例让IOC容器创建--autofac来创建
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

#endregion Autofac

builder.Services.AddHangfire(builder.Configuration);

GlobalConfig.Services = builder.Services;
GlobalConfig.Configuration = builder.Configuration;
GlobalConfig.HostEnvironment = builder.Environment;
 
var app = builder.Build();

//app.UseMiddleware(typeof(SwaggerAuthMiddleware));
app.UseStaticFiles();
 
app.UseHttpsRedirection();

app.UseCookiePolicy();
app.UseSession();

app.UseSwaggerAuthorized(); 
 
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
}

app.UseMiddleware(typeof(VisitRecordMiddleware));

app.UseAuthorization();
 
app.MapControllers();

app.UseHangfire();
  
GlobalConfig.ServiceProvider = app.Services;
 
app.Run();