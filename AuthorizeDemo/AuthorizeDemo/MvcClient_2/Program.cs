using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
//添加session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //session活期时间
    options.Cookie.HttpOnly = true;//设为httponly
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //登入地址
                options.LoginPath = "/Account/Index/";
                //登出地址
                options.LogoutPath = "/Account/Logout/";
                options.Cookie.Name = "MvcClient_2_SessionId";
                options.Cookie.HttpOnly = true;
                //设置cookie过期时长
                options.ExpireTimeSpan = TimeSpan.FromSeconds(1);
            });

//配置session保存到redis中
//要想在net core中实现分布式session,实现单点登录，我们可以把session保存到redis中，这样就可以多个项目共享
builder.Services.AddDistributedRedisCache(options =>
{
    //用于连接Redis的配置  Configuration.GetConnectionString("RedisConnectionString")读取配置信息的连接字符串
    options.Configuration = "localhost:6379";
    //Redis实例名DemoInstance
    options.InstanceName = "MvcClient_2_Instance";
});

builder.Services.AddDataProtection(opt => {
    opt.ApplicationDiscriminator = "MvcClient";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//下面两个中间件顺序不能颠倒
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
