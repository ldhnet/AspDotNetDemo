using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = "http://localhost:5200";
        options.RequireHttpsMetadata = false;
        options.ClientId = "mvc client";
        options.ClientSecret = "mvc secret";
        options.SaveTokens = true;
        options.ResponseType = "code";

        options.Scope.Clear();
        options.Scope.Add("api1");
        options.Scope.Add(OidcConstants.StandardScopes.OpenId);
        options.Scope.Add(OidcConstants.StandardScopes.Profile);
        options.Scope.Add(OidcConstants.StandardScopes.Email);
        options.Scope.Add(OidcConstants.StandardScopes.Phone);
        options.Scope.Add(OidcConstants.StandardScopes.Address);
        options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
