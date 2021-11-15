using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Localization; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; 
using System; 
using System.Globalization;
using System.IO; 
using WebMVC.Attributes; 
using WebMVC.Extension;
using WebMVC.Filter;
using WebMVC.HangFire;
using WebMVC.Helper;
using Framework.Utility.Config;
using WebMVC.Model;  
using Framework.Core.Data; 
using System.Linq;
using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using DirectService.Admin.Impl;
using LangResources;

namespace WebMVC
{
    public class Startup
    { 
        public IConfiguration Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        { 
            GlobalContext.HostingEnvironment = env;
        } 
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {  
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddOptions();

           // services.AddLocalization();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #region config
            if (Configuration == null)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

                var aaaaa = GlobalContext.HostingEnvironment.EnvironmentName;

                if (GlobalContext.HostingEnvironment.IsEnvironment("Development"))
                {
                    configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                }
                else
                {
                    configurationBuilder.AddJsonFile("appsettings.Qa.json", optional: true, reloadOnChange: true);
                }
                Configuration = configurationBuilder.Build();
                services.AddSingleton(Configuration);
            }
            GlobalConfig.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();
            services.Configure<SystemConfig>(Configuration.GetSection("SystemConfig"));
            GlobalContext.Services = services;
            GlobalContext.Configuration = Configuration;
            #endregion

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

            ProviderManage.MemoryCacheProvider = new MemoryCacheProvider(); 

            services.AddSingleton<MyFilter>();

            services.AddSingleton<LangResource>();


            services.AddHangfire(r => r.UseSqlServerStorage(GlobalConfig.SystemConfig.DBConnectionString));
             
            //注册Cookie认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                //options.Filters.Add<MyAuthorizeFilter>();
                options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());

                //options.CacheProfiles.Add("test1", new CacheProfile()
                //{
                //    Duration = 5
                //});
                //options.CacheProfiles.Add("test2", new CacheProfile()
                //{
                //    Location = ResponseCacheLocation.None,
                //    NoStore = true
                //}); 
            });
            //    .AddNewtonsoftJson(options =>
            //{
            //    // 返回数据首字母不小写，CamelCasePropertyNamesContractResolver是小写
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});

            services.AddMiddlewares();

            services.AddSingleton<IRepository<EmployeeLogin, int>, Repository<EmployeeLogin, int>>();
            services.AddSingleton<IRepository<Employee, int>, Repository<Employee, int>>();
            services.AddSingleton<IRepository<SysAccount, int>, Repository<SysAccount, int>>();
            services.AddSingleton<IUnitOfWork, MyDBContext>();

            services.AddSingleton<IUserService, UserService>();
        
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

            #region  Autofac

            //services.AddAutofac(containerBuilder => { 
            //    containerBuilder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));

            //    var baseType = typeof(IDependency);
            //    var serviceAssembly =
            //        Assembly
            //            .GetEntryAssembly()//获取默认程序集
            //            .GetReferencedAssemblies()//获取所有引用程序集
            //            .Select(Assembly.Load)
            //            .Where(c => c.FullName.Contains("DirectService", StringComparison.OrdinalIgnoreCase))
            //            .ToArray();

            //    containerBuilder.RegisterAssemblyTypes(serviceAssembly)
            //        .Where(type => baseType.IsAssignableFrom(baseType) && !type.IsAbstract)
            //        .AsSelf()   //自身服务，用于没有接口的类
            //        .AsImplementedInterfaces()  //接口服务
            //        .PropertiesAutowired()  //属性注入
            //        .SingleInstance();    //保证生命周期基于请求

            //    containerBuilder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //    //将services中的服务填充到Autofac中.
            //    containerBuilder.Populate(services);
            //    //创建容器.
            //    containerBuilder.Build(); 
            //}); 
            #endregion Autofac

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

            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            }); 

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

            //app.UseMiddleware(typeof(ResponseHeaderMiddleware)); 

            app.UseAuthentication();
            app.UseAuthorization();

           
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
