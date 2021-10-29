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
using WebMVC.Common;
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
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddOptions();

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
             
            //注册Cookie认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                //options.Filters.Add<MyAuthorizeFilter>();
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            }).AddNewtonsoftJson(options =>
            {
                // 返回数据首字母不小写，CamelCasePropertyNamesContractResolver是小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMiddlewares();

            #region 最大请求数

            ////使用栈策略模式
            //services.AddStackPolicy(options =>
            //{
            //    //最大并发请求数,超过之后,进行排队
            //    options.MaxConcurrentRequests = 3;

            //    //最大请求数,超过之后,返回503
            //    options.RequestQueueLimit = 100;
            //});

            ////使用队列策略模式
            //services.AddQueuePolicy(options =>
            //{
            //    //最大并发请求数,超过之后,进行排队
            //    options.MaxConcurrentRequests = 100;
            //    //最大请求数,超过之后,返回503
            //    options.RequestQueueLimit = 100;

            //});

            ////如果这两个策略同时使用,后面的策略模式会覆盖上边的策略模式

            #endregion

            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();

            services.Configure<SystemConfig>(Configuration.GetSection("SystemConfig")); 
            GlobalContext.Services = services;
            GlobalContext.Configuration = Configuration;
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
            app.UseStaticHostEnviroment();
            app.UseRouting();
            app.UseSession();

            //启用并发限制数中间件
            //app.UseConcurrencyLimiter();

            var jobOptions = new BackgroundJobServerOptions
            {
                Queues = new[] { "back", "front", "default" },//队列名称，只能为小写
                WorkerCount = Environment.ProcessorCount * 1, //并发任务数
                ServerName = "conference hangfire1",//服务器名称
            };
            app.UseHangfireServer(jobOptions);
            //app.UseHangfireDashboard();

            ////控制仪表盘的访问路径和授权配置
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyDashboardAuthorizationFilter() }
            }); 
            HangFireJob.AddOrUpdate();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware(typeof(ResponseHeaderMiddleware)); 
            app.UseMiddlewares();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });

            
            GlobalContext.ServiceProvider = app.ApplicationServices;

        }
    }
}
