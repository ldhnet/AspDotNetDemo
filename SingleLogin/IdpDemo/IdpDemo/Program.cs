
global using System;
global using MyEnv = System.Environment;
using IdpDemo;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5900");
// Add services to the container.
builder.Services.AddControllersWithViews();

TestUsers._Configuration = builder.Configuration;

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    TestUsers._Environment = hostingContext.HostingEnvironment;
     

    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    config.AddEnvironmentVariables();
});
 
var idpBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    //Ĭ�ϵĵ�½ҳ����/account/login
    //options.UserInteraction.LoginUrl = "/login";
    //��Ȩȷ��ҳ�� Ĭ��/consent
    //options.UserInteraction.ConsentUrl = "";

}).AddTestUsers(TestUsers.Users); 
idpBuilder.AddInMemoryIdentityResources(Config.GetIdentityResources());
idpBuilder.AddInMemoryApiResources(Config.GetApis());
idpBuilder.AddInMemoryClients(Config.GetClients());
idpBuilder.AddInMemoryApiScopes(Config.ApiScopes);  //���ApiScopes��Ҫ�¼��ϣ����������ʾinvalid_scope
  
//idpBuilder.AddDeveloperSigningCredential();//false, "tempkey.rsa"

//builder.Services.AddCors(options => options.AddPolicy("cors", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


if (TestUsers._Environment.IsDevelopment())
{
    idpBuilder.AddDeveloperSigningCredential(true, "tempkey.rsa");
}
else
{
    var signingCredential = new TestUsers().CreateSigningCredential();
    idpBuilder.AddSigningCredential(signingCredential);

    //idpBuilder.AddSigningCredential(new System.Security.Cryptography.X509Certificates.X509Certificate2(Path.Combine(Environment.ContentRootPath,Configuration["Certificates:CertPath"]),Configuration["Certificates:Password"]));
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

//app.UseCors("cors"); 
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
