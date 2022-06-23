using IdentityServer4.AccessTokenValidation;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5901");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

  
//builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
//.AddIdentityServerAuthentication(options =>
//{
//    options.Authority = "http://localhost:5900";
//    options.ApiName = "api1";
//    options.RequireHttpsMetadata = false;
//    options.ApiSecret = "api1 secret";
//});


builder.Services.AddAuthentication("Bearer")
.AddJwtBearer("Bearer", options =>
{
    options.Authority = "http://localhost:5900";
    options.RequireHttpsMetadata = false;
    options.Audience = "api1";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Policy", builder =>
    {
        builder.RequireAuthenticatedUser();//验证是否登录
        builder.RequireClaim("scope","api1");
    });
});

builder.Services.AddMemoryCache();
builder.Services.AddCors(options =>options.AddPolicy("cors",p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("cors");
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();//.RequireAuthorization("Policy") 所有controller 使用授权
//});

app.Run();
