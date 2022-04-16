using Lee.EF.Context;
using Lee.Repository.Data;
using Lee.Utility.Config;
using Lee.Utility.Helper;
using Lee.Utility.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using WebA.Admin.Contracts;
using WebA.Admin.Service;
using WebA.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region config

if (builder.Environment.IsEnvironment("Development"))
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
}
else if (builder.Environment.IsEnvironment("Qa"))
{
    builder.Configuration.AddJsonFile("appsettings.Qa.json", optional: true, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
}
GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();
#endregion

builder.WebHost.UseUrls("http://localhost:5180");

Console.WriteLine(builder.Configuration.GetValue<string>("EnvironmentName"));

Console.WriteLine(builder.Configuration.GetValue<string>("SystemConfig:Demo"));
 
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false; //这里要改为false，默认是true，true的时候session无效
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

#region DataProtection

//部署同一服务器
builder.Services.AddDataProtection(configure => {
    configure.ApplicationDiscriminator = "commonwebmvc";
}).SetApplicationName("commonweb")
    //windows、Linux、macOS 下可以使用此种方式 保存到文件系统
    .PersistKeysToFileSystem(new System.IO.DirectoryInfo("C:\\share_keys"));


////部署不同服务器
//builder.Services.AddDataProtection(configure =>
//{
//    configure.ApplicationDiscriminator = "commonwebmvc";
//}).SetApplicationName("commonweb1")
//.AddKeyManagementOptions(options =>
//{
//    //配置自定义XmlRepository
//    options.XmlRepository = new XmlRepository();
//});
#endregion

#region 使用Redis保存Session


//将Redis分布式缓存服务添加到服务中
//builder.Services.AddDistributedRedisCache(options =>
//{
//    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
//    {
//        Password = "我不是密码",
//        ConnectTimeout = 5000,//设置建立连接到Redis服务器的超时时间为5000毫秒
//        SyncTimeout = 5000,//设置对Redis服务器进行同步操作的超时时间为5000毫秒
//        ResponseTimeout = 5000//设置对Redis服务器进行操作的响应超时时间为5000毫秒
//    };

//    options.ConfigurationOptions.EndPoints.Add("localhost:6379");
//    options.InstanceName = "DemoInstance";
//});


// 这里取连接字符串
builder.Services.AddDistributedRedisCache(option =>
{
    //redis 连接字符串
    option.Configuration = "127.0.0.1:6379";
    //redis 实例名
    option.InstanceName = "demo_Session";
});

//添加session 设置过期时长分钟 
//var sessionOutTime = con.ConnectionConfig.ConnectionRedis.SessionTimeOut;
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(Convert.ToDouble(3 * 60 * 60)); //session活期时间
    options.Cookie.HttpOnly = true;//设为httponly
});
#endregion

//builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthInfo.CookieInstance)
                .AddCookie(CookieAuthInfo.CookieInstance, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Denied");
                    options.LogoutPath = new PathString("/Account/Logout");
                });

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<MyDBContext>();

builder.Services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>));

builder.Services.AddSingleton<IEmployeeContract,EmployeeService>();

GlobalConfig.Services = builder.Services;
GlobalConfig.Configuration = builder.Configuration;
GlobalConfig.HostEnvironment = builder.Environment;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

GlobalConfig.ServiceProvider = app.Services;
app.Run();
