using Lee.Utility.Helper;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false; //这里要改为false，默认是true，true的时候session无效
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

builder.Services.AddDataProtection(configure =>
{
    configure.ApplicationDiscriminator = "commonwebmvc";
}).SetApplicationName("commonweb2")

.AddKeyManagementOptions(options =>
{
    //配置自定义XmlRepository
    options.XmlRepository = new XmlRepository();
});

//services.AddSession();
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
    option.InstanceName = "Test_Session";
});

//添加session 设置过期时长分钟 
//var sessionOutTime = con.ConnectionConfig.ConnectionRedis.SessionTimeOut;
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(Convert.ToDouble(3 * 60 * 60)); //session活期时间
    options.Cookie.HttpOnly = true;//设为httponly
});
#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
