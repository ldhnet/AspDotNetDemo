using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MvcClient.Config;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5902");

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

GlobalContext.IdpClients = builder.Configuration.GetSection("IdpClients").Get<IdpClients>();
 

builder.Services.AddAuthorization(); 

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "oidc";//OpenIdConnectDefaults.AuthenticationScheme
}).AddCookie(options =>
{
    options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.AccessDeniedPath = "/Authorization/AccessDenied";
}).AddOpenIdConnect("oidc", options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = GlobalContext.IdpClients.Authority;
    options.ClientId = GlobalContext.IdpClients.ClientId;
    options.ClientSecret = GlobalContext.IdpClients.ClientSecrets;
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.RequireHttpsMetadata = false;
     
    //添加申请 权限 ，这里的 scope 必须在授权服务端定义的客户端 AllowedScopes 里
    options.Scope.Clear();
    options.Scope.Add("api1");
    options.Scope.Add(OidcConstants.StandardScopes.OpenId);
    options.Scope.Add(OidcConstants.StandardScopes.Email);
    options.Scope.Add(OidcConstants.StandardScopes.Address);
    options.Scope.Add(OidcConstants.StandardScopes.Phone);
    options.Scope.Add(OidcConstants.StandardScopes.Profile);
    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

    //授权失败 跳转到错误引导页面
    options.Events = new OpenIdConnectEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/");
            context.Response.WriteAsync("验证失败");
            return Task.CompletedTask;
        }
    };
});

#region
//.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
//                {
//                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                    options.Authority = "http://localhost:5900";
//                    options.RequireHttpsMetadata = false;

//                    options.ClientId = "CodeClient";
//                    options.ClientSecret = "hybrid secret";
//                    options.SaveTokens = true;
//                    options.ResponseType = "code id_token";

//                    options.Scope.Clear();

//                    options.Scope.Add("api1");
//                    options.Scope.Add(OidcConstants.StandardScopes.OpenId);
//                    options.Scope.Add(OidcConstants.StandardScopes.Profile);
//                    options.Scope.Add(OidcConstants.StandardScopes.Email);
//                    options.Scope.Add(OidcConstants.StandardScopes.Phone);
//                    options.Scope.Add(OidcConstants.StandardScopes.Address);
//                    options.Scope.Add("roles");
//                    options.Scope.Add("locations");

//                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

//                    // 集合里的东西 都是要被过滤掉的属性，nbf amr exp...
//                    options.ClaimActions.Remove("nbf");
//                    options.ClaimActions.Remove("amr");
//                    options.ClaimActions.Remove("exp");

//                    // 不映射到User Claims里
//                    //options.ClaimActions.DeleteClaim("sid");
//                    //options.ClaimActions.DeleteClaim("sub");
//                    //options.ClaimActions.DeleteClaim("idp");

//                    //// 让Claim里面的角色成为mvc系统识别的角色
//                    //options.TokenValidationParameters = new TokenValidationParameters
//                    //{
//                    //    NameClaimType = JwtClaimTypes.Name,
//                    //    RoleClaimType = JwtClaimTypes.Role
//                    //};

//                    //授权失败 跳转到错误引导页面
//                    options.Events = new OpenIdConnectEvents() { 
//                        OnRemoteFailure = context => {
//                            context.Response.Redirect("/");
//                            context.HandleResponse();
//                            return Task.CompletedTask;
//                        }
//                    };

//                });
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
