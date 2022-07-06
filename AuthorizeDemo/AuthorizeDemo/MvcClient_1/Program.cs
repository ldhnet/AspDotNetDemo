using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
//普通用户登录

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //登入地址
                options.LoginPath = "/Account/Index/";
                //登出地址
                options.LogoutPath = "/Account/Logout/";
                options.Cookie.Name = "My_SessionId";
                options.Cookie.HttpOnly = true;
                //设置cookie过期时长
                //options.ExpireTimeSpan = TimeSpan.FromSeconds(10);
            });
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
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
//    RequestPath = "/wwwroot"
//});

//app.UseCookiePolicy();
app.UseRouting();

//下面两个中间件顺序不能颠倒
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
