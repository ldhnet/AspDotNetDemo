using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
//���session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //session����ʱ��
    options.Cookie.HttpOnly = true;//��Ϊhttponly
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //�����ַ
                options.LoginPath = "/Account/Index/";
                //�ǳ���ַ
                options.LogoutPath = "/Account/Logout/";
                options.Cookie.Name = "MvcClient_2_SessionId";
                options.Cookie.HttpOnly = true;
                //����cookie����ʱ��
                options.ExpireTimeSpan = TimeSpan.FromSeconds(1);
            });

//����session���浽redis��
//Ҫ����net core��ʵ�ֲַ�ʽsession,ʵ�ֵ����¼�����ǿ��԰�session���浽redis�У������Ϳ��Զ����Ŀ����
builder.Services.AddDistributedRedisCache(options =>
{
    //��������Redis������  Configuration.GetConnectionString("RedisConnectionString")��ȡ������Ϣ�������ַ���
    options.Configuration = "localhost:6379";
    //Redisʵ����DemoInstance
    options.InstanceName = "MvcClient_2_Instance";
});

builder.Services.AddDataProtection(opt => {
    opt.ApplicationDiscriminator = "MvcClient";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//���������м��˳���ܵߵ�
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
