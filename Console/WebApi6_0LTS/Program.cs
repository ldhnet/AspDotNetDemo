using Autofac;
using Autofac.Extensions.DependencyInjection;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Mapping; 
using WebApi6_0.AutofacConfig;
using WebApi6_0.Filter;

var builder = WebApplication.CreateBuilder(args);
// Look for static files in webroot
//builder.WebHost.UseWebRoot("webroot");

builder.WebHost.UseUrls("https://*:9080", "http://*:9081");

// Wait 30 seconds for graceful shutdown.
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

GlobalConfig.SystemConfig = builder.Configuration.GetSection("SystemConfig").Get<SystemConfig>();

// Add services to the container.

//�������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApi6_0_����api", Version = "v1" });
    // ���ô�xml�ĵ��л�ȡ������Ϣ
    // ·�����ǻ�ȡ����Ŀ·��+startup�����ռ䣨Ҳ����ֱ��д���ɵ�xml���ƣ�
    var filePath = Path.Combine(System.AppContext.BaseDirectory, typeof(Program).Assembly.GetName().Name + ".xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers(options => {
    options.Filters.Add<TokenCheckFilter>();
}); 

builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddAutoMapper();
  
#region  Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ConfigureAutofac()));
  
#endregion Autofac

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi6_0 v1"); c.RoutePrefix = string.Empty; });
}

app.UseHttpsRedirection();

app.UseAuthorization();
//�������
app.UseCors("CorsPolicy");
 
app.UseMapperAutoInject();

app.UseShardResource();

app.MapControllers();

app.Run();
