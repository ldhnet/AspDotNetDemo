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

#region ʹ��Redis����Session
// ʹ��SqlServer����Session
//builder.Services.AddSqlServerCache(o =>
//{
//    o.ConnectionString = "Server=.;Database=test;Trusted_Connection=True;";
//    o.SchemaName = "dbo";
//    o.TableName = "Sessions";
//});


// ʹ��Redis����Session
builder.Services.AddDistributedRedisCache(option =>
{
    //redis �����ַ���
    option.Configuration = "127.0.0.1:6379";
    //redis ʵ����
    option.InstanceName = "ApiA";
});

// ע��Session����
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Lee.Session";
    options.IdleTimeout = System.TimeSpan.FromSeconds(30 * 60);//����session�Ĺ���ʱ�� ��λ��
    options.Cookie.HttpOnly = true;//���������������ͨ��js��ø�cookie��ֵ
});

#endregion


#region ����
builder.Services.AddCors(options => options.AddPolicy("AllowSameDomain",builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApiA_����api", Version = "v1" });
    // ���ô�xml�ĵ��л�ȡ������Ϣ
    // ·�����ǻ�ȡ����Ŀ·��+startup�����ռ䣨Ҳ����ֱ��д���ɵ�xml���ƣ�
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
