using Autofac;
using Autofac.Extensions.DependencyInjection;
using DH.Models.DbModels;
using Framework.Utility.Config;
using Microsoft.EntityFrameworkCore;
using WebApi6_0.AutofacConfig;

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


builder.Services.AddDbContextPool<MyDBContext>(options =>
{
    var strConnection = GlobalConfig.SystemConfig.DBConnectionString;
    options.UseSqlServer(strConnection);
}, 64);



#region  Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));

//builder.Host.ConfigureContainer<ContainerBuilder>(builder => { 
//    Type baseType = typeof(IDependency);
//    Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "WebMVC.dll").Select(m => Assembly.LoadFrom(m)).ToArray();
//    builder.RegisterAssemblyTypes(assemblies).Where(type => baseType.IsAssignableFrom(type)).AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope(); 
//});


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
