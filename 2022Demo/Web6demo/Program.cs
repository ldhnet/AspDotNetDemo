using Lee.Utility.Helper;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false; //����Ҫ��Ϊfalse��Ĭ����true��true��ʱ��session��Ч
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

builder.Services.AddDataProtection(configure =>
{
    configure.ApplicationDiscriminator = "commonwebmvc";
}).SetApplicationName("commonweb2")

.AddKeyManagementOptions(options =>
{
    //�����Զ���XmlRepository
    options.XmlRepository = new XmlRepository();
});

//services.AddSession();
#region ʹ��Redis����Session


//��Redis�ֲ�ʽ���������ӵ�������
//builder.Services.AddDistributedRedisCache(options =>
//{
//    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
//    {
//        Password = "�Ҳ�������",
//        ConnectTimeout = 5000,//���ý������ӵ�Redis�������ĳ�ʱʱ��Ϊ5000����
//        SyncTimeout = 5000,//���ö�Redis����������ͬ�������ĳ�ʱʱ��Ϊ5000����
//        ResponseTimeout = 5000//���ö�Redis���������в�������Ӧ��ʱʱ��Ϊ5000����
//    };

//    options.ConfigurationOptions.EndPoints.Add("localhost:6379");
//    options.InstanceName = "DemoInstance";
//});


// ����ȡ�����ַ���
builder.Services.AddDistributedRedisCache(option =>
{
    //redis �����ַ���
    option.Configuration = "127.0.0.1:6379";
    //redis ʵ����
    option.InstanceName = "Test_Session";
});

//���session ���ù���ʱ������ 
//var sessionOutTime = con.ConnectionConfig.ConnectionRedis.SessionTimeOut;
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(Convert.ToDouble(3 * 60 * 60)); //session����ʱ��
    options.Cookie.HttpOnly = true;//��Ϊhttponly
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
