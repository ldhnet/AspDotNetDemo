using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
//��ͨ�û���¼

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //�����ַ
                options.LoginPath = "/Account/Index/";
                //�ǳ���ַ
                options.LogoutPath = "/Account/Logout/";
                options.Cookie.Name = "My_SessionId";
                options.Cookie.HttpOnly = true;
                //����cookie����ʱ��
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

//���������м��˳���ܵߵ�
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
