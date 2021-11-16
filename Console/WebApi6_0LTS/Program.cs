using Autofac;
using Autofac.Extensions.DependencyInjection;
using DH.Models.DbModels;
using Framework.Core.Data;
using Framework.Core.Dependency;
using Framework.Utility.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Look for static files in webroot
//builder.WebHost.UseWebRoot("webroot");

builder.WebHost.UseUrls("https://*:9080", "http://*:9081");

// Wait 30 seconds for graceful shutdown.
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

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


#region  Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    Type baseType = typeof(IDependency);
    var assemblies = Assembly.GetEntryAssembly()?//获取默认程序集
            .GetReferencedAssemblies()//获取所有引用程序集
            .Select(Assembly.Load)
            .Where(c => c.FullName.Contains("DirectService", StringComparison.OrdinalIgnoreCase))
            .ToArray();
    containerBuilder.RegisterAssemblyTypes(assemblies)
        .Where(type => baseType.IsAssignableFrom(baseType) && !type.IsAbstract)
        .AsSelf()   //自身服务，用于没有接口的类
        .AsImplementedInterfaces()  //接口服务
        .PropertiesAutowired()  //属性注入
        .InstancePerLifetimeScope();    //保证生命周期基于请求  

    containerBuilder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
    containerBuilder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
});


#endregion Autofac

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "WebApi6_0", Version = "v1" });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

app.UseHttpsRedirection();

app.UseAuthorization();
//解决跨域
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
