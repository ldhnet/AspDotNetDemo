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

//kestrel�������������Ϊ��28M��
//formbody���Ϊ��128M

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:9010");
// Add services to the container.
builder.WebHost.UseWebRoot("wwwroot");
//var currentDir = Directory.GetCurrentDirectory();
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

//���kestrel����
builder.WebHost.ConfigureKestrel(options =>
{
    //options.Limits.MaxRequestBodySize = 30000000L;//Ĭ��Լ28M
    //options.Limits.MaxRequestBodySize = 2 * 2L << 30;//ָ�����2G
    options.Limits.MaxRequestBodySize = null;//ȥ������ ��������ʾ
});

//��� formbody����
builder.Services.Configure<FormOptions>(x =>
{
    //x.MultipartBodyLengthLimit = 134217728;//Ĭ��128MB
    x.MultipartBodyLengthLimit = 5 * 2L << 30;//�����ֶ�����Ϊ5GB,��ô�����ֵ��������ʾ
}); 


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDataProtection();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region ʹ��Redis����Session
// ʹ��SqlServer����Session
//builder.Services.AddSqlServerCache(o =>
//{
//    o.ConnectionString = "Server=.;Database=test;Trusted_Connection=True;";
//    o.SchemaName = "dbo";
//    o.TableName = "Sessions";
//});


//// ʹ��Redis����Session
//builder.Services.AddDistributedRedisCache(option =>
//{
//    //redis �����ַ���
//    option.Configuration = "127.0.0.1:6379";
//    //redis ʵ����
//    option.InstanceName = "ApiA";
//});

// ע��Session����
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Lee.Session";
    options.IdleTimeout = System.TimeSpan.FromSeconds(30 * 60);//����session�Ĺ���ʱ�� ��λ��
    options.Cookie.HttpOnly = true;//���������������ͨ��js��ø�cookie��ֵ
});

#endregion

#region ����
builder.Services.AddCors(options => options.AddPolicy("AllowSameDomain",builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
#endregion

builder.Services.Configure<FormOptions>(options => {
    options.ValueLengthLimit = 209715200;//200MB   //.netcore ������ÿ�� POST ����ֵ�ĳ���Ϊ 4M  ������200M
});

//builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApiA_����api", Version = "v1" });
    // ���ô�xml�ĵ��л�ȡ������Ϣ
    // ·�����ǻ�ȡ����Ŀ·��+startup�����ռ䣨Ҳ����ֱ��д���ɵ�xml���ƣ�
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
    builder.RegisterTypes(assemblieTypes).AsSelf().InstancePerLifetimeScope(); //��֤�������ڻ������� 


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


    var assemblies = Assembly.GetEntryAssembly()?//��ȡĬ�ϳ���
            .GetReferencedAssemblies()//��ȡ�������ó���
            .Select(Assembly.Load)
            .Where(c => c.FullName!.Contains("WebA.Admin", StringComparison.OrdinalIgnoreCase))
            .ToArray();

    builder.RegisterAssemblyTypes(assemblies!)
        .Where(type => !type.IsAbstract)
        .AsSelf()   //�����������û�нӿڵ���
        .AsImplementedInterfaces()  //�ӿڷ���
        .PropertiesAutowired()  //����ע��
        .InstancePerLifetimeScope(); //��֤�������ڻ������� 

    //֧������ע��
    var controllerBaseType = typeof(ControllerBase);
    builder.RegisterAssemblyTypes(typeof(Program).Assembly)
    .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
    .PropertiesAutowired(new CustomPropertySelector());//֧������ע��
});

//֧��������ʵ����IOC��������--autofac������
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