using IdpDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var idpBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
          .AddTestUsers(TestUsers.Users);

// in-memory, code config
idpBuilder.AddInMemoryIdentityResources(Config.GetIdentityResources());
idpBuilder.AddInMemoryApiResources(Config.GetApis());

idpBuilder.AddInMemoryApiScopes(Config.GetScopes());

idpBuilder.AddInMemoryClients(Config.GetClients());



idpBuilder.AddDeveloperSigningCredential();


//if (Environment.IsDevelopment())
//{
//    idpBuilder.AddDeveloperSigningCredential();
//}
//else
//{
//    throw new Exception("need to configure key material");
//}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
