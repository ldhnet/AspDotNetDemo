using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Attributes;
using WebMVC.Extension;
using WebMVC.Filter;
using WebMVC.HangFire;
using WebMVC.Middleware;
using WebMVC.Model;
using WebMVC.Service;

namespace WebMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            GlobalContext.HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();

            services.Configure<SystemConfig>(Configuration.GetSection("SystemConfig"));


            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                var loggerFactory = container.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

                return new ClientIpCheckActionFilter(logger);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

         
            services.AddSingleton<MyFilter>();
             
            services.AddHangfire(r => r.UseSqlServerStorage(GlobalContext.SystemConfig.DBConnectionString));
             
            //ע��Cookie��֤����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                //options.Filters.Add<MyAuthorizeFilter>();
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            }).AddNewtonsoftJson(options =>
            {
                // ������������ĸ��Сд��CamelCasePropertyNamesContractResolver��Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            var jobOptions = new BackgroundJobServerOptions
            {
                Queues = new[] { "back", "front", "default" },//�������ƣ�ֻ��ΪСд
                WorkerCount = Environment.ProcessorCount * 1, //����������
                ServerName = "conference hangfire1",//����������
            };
            app.UseHangfireServer(jobOptions);
            //app.UseHangfireDashboard();

            ////�����Ǳ��̵ķ���·������Ȩ����
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyDashboardAuthorizationFilter() }
            });
             
             
            app.UseStaticHostEnviroment();

            HangFireJob.AddOrUpdate();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware(typeof(ResponseHeaderMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
