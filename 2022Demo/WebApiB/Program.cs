using Lee.Hangfire;
using Lee.Utility.Config;
using Microsoft.AspNetCore.Http.Features; 
using WebApiB.Code;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5099");//�м�Ҫ��* ����Ҫ��localhost ����docker �޷����� �����ӱ����ô���
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
    config.AddEnvironmentVariables();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<SendEmailConfig>();
builder.Services.AddSingleton<ISendEmailManager,SendEmailManager>();

builder.Services.AddHostedService<SendEmailHost>();



builder.Services.Configure<FormOptions>(options => {
    options.ValueLengthLimit = 209715200;//200MB   //.netcore ������ÿ�� POST ����ֵ�ĳ���Ϊ 4M  ������200M
});

//builder.Services.AddHangfire(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

//app.UseHangfire();

GlobalConfig.ServiceProvider = app.Services;

app.Run();
