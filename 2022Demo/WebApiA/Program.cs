using WebA.Admin;
using WebA.Admin.Contracts;
using WebA.Admin.Service;
using WebApiA.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5015");
// Add services to the container.
builder.WebHost.UseWebRoot("wwwroot");
//var currentDir = Directory.GetCurrentDirectory();
builder.Host.UseContentRoot(Directory.GetCurrentDirectory());


builder.Services.AddDistributedMemoryCache();
builder.Services.AddDataProtection();

#region 使用Redis保存Session
// 使用SqlServer保存Session
//builder.Services.AddSqlServerCache(o =>
//{
//    o.ConnectionString = "Server=.;Database=test;Trusted_Connection=True;";
//    o.SchemaName = "dbo";
//    o.TableName = "Sessions";
//});


// 使用Redis保存Session
builder.Services.AddDistributedRedisCache(option =>
{
    //redis 连接字符串
    option.Configuration = "127.0.0.1:6379";
    //redis 实例名
    option.InstanceName = "ApiA";
});

// 注册Session服务
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Lee.Session";
    options.IdleTimeout = System.TimeSpan.FromSeconds(30 * 60);//设置session的过期时间 单位秒
    options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
});

#endregion


#region 跨域
builder.Services.AddCors(options => options.AddPolicy("AllowSameDomain",builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApiA_测试api", Version = "v1" });
    // 配置从xml文档中获取描述信息
    // 路径我们获取的项目路径+startup命名空间（也可以直接写生成的xml名称）
    var filePath = Path.Combine(System.AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
    c.IncludeXmlComments(filePath);
});


builder.Services.AddSingleton<ISystemContract, SystemService>();
builder.Services.AddSingleton<ServiceContext>();

GlobalConfig.Services = builder.Services;
GlobalConfig.Configuration = builder.Configuration;
GlobalConfig.HostEnvironment = builder.Environment;

var app = builder.Build();
//app.UseMiddleware(typeof(SwaggerAuthMiddleware));
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCookiePolicy();
app.UseSession();

app.UseSwaggerAuthorized(); 
 
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
}
 
app.UseAuthorization();
 
app.MapControllers();
GlobalConfig.ServiceProvider = app.Services;

app.Run();
