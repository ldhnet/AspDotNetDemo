using DongHui.OAuth.Core.Models;
using DongHui.OAuth.Gitee;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.AddSingleton(new GiteeOAuth(OAuthConfig.LoadFrom(builder.Configuration, "oauth:gitee")));
 
var app = builder.Build();

app.UseSession();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
