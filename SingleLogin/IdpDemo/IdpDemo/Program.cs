
global using System;
global using MyEnv = System.Environment;
using IdpDemo;
using IdpDemo.Models;
using IdpDemo.Services;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5900");
// Add services to the container.
builder.Services.AddControllersWithViews();

GlobalContext._Configuration = builder.Configuration;

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    GlobalContext._Environment = hostingContext.HostingEnvironment;
       
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true); 
    config.AddEnvironmentVariables();
});

GlobalContext.IdpClients_mvc = builder.Configuration.GetSection("IdpClients_mvc").Get<IdpClients_mvc>();
GlobalContext.IdpClients_mvc_test = builder.Configuration.GetSection("IdpClients_mvc_test").Get<IdpClients_mvc_test>();


builder.Services.AddSingleton<IUserInterface, UserService>();

var idpBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    //默认的登陆页面是/account/login
    //options.UserInteraction.LoginUrl = "/login";
    //授权确认页面 默认/consent
    //options.UserInteraction.ConsentUrl = "";

});//.AddTestUsers(TestUsers.Users); 
idpBuilder.AddInMemoryIdentityResources(Config.GetIdentityResources());
idpBuilder.AddInMemoryApiResources(Config.GetApis());
idpBuilder.AddInMemoryClients(Config.GetClients());
idpBuilder.AddInMemoryApiScopes(Config.ApiScopes);  //这个ApiScopes需要新加上，否则访问提示invalid_scope
  
//idpBuilder.AddDeveloperSigningCredential();//false, "tempkey.rsa"

if (GlobalContext._Environment.IsDevelopment())
{
    idpBuilder.AddDeveloperSigningCredential(true, "tempkey.rsa");
}
else
{
    var signingCredential = new SigningCredentialConfig().CreateSigningCredential();
    idpBuilder.AddSigningCredential(signingCredential);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
 
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Demo}/{action=Index}");

app.Run();
