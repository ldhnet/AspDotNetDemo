using DH.Models.Entities;
using DirectService.Admin.Contracts;
using DirectService.Admin.Service;
using Framework.Core.Data;
using Framework.EF.Context;
using Framework.Utility;
using Framework.Utility.Config;
using Hangfire;
using LangResources;
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
using WebMVC.Model;

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


            var aaa= LangResource.AnyRadixConvert_Overflow;

            var bbb = LangResource.CheckNotNull;
            var ccc = LangResource.Caching_CacheNotInitialized;

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

            services.AddHangfire(r => r.UseSqlServerStorage(GlobalConfig.SystemConfig.DBConnectionString));

            //????Cookie????????
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
            //    // ??????????????????????CamelCasePropertyNamesContractResolver??????
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});

            services.AddMiddlewares();

            services.AddSingleton<IRepository<EmployeeLogin, int>, Repository<EmployeeLogin, int>>();
            services.AddSingleton<IRepository<Employee, int>, Repository<Employee, int>>();
            services.AddSingleton<IRepository<SysAccount, int>, Repository<SysAccount, int>>();
            services.AddSingleton<IUnitOfWork, MyDBContext>();

            services.AddSingleton<IUserService, UserService>();

            #region ??????????

            ////??????????????
            //services.AddStackPolicy(options =>
            //{
            //    //??????????????,????????,????????
            //    options.MaxConcurrentRequests = 3;

            //    //??????????,????????,????503
            //    options.RequestQueueLimit = 100;
            //});

            ////????????????????
            //services.AddQueuePolicy(options =>
            //{
            //    //??????????????,????????,????????
            //    options.MaxConcurrentRequests = 100;
            //    //??????????,????????,????503
            //    options.RequestQueueLimit = 100;

            //});

            ////??????????????????????,??????????????????????????????????

            #endregion

            #region  Autofac

            //services.AddAutofac(containerBuilder => { 
            //    containerBuilder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));

            //    var baseType = typeof(IDependency);
            //    var serviceAssembly =
            //        Assembly
            //            .GetEntryAssembly()//??????????????
            //            .GetReferencedAssemblies()//??????????????????
            //            .Select(Assembly.Load)
            //            .Where(c => c.FullName.Contains("DirectService", StringComparison.OrdinalIgnoreCase))
            //            .ToArray();

            //    containerBuilder.RegisterAssemblyTypes(serviceAssembly)
            //        .Where(type => baseType.IsAssignableFrom(baseType) && !type.IsAbstract)
            //        .AsSelf()   //??????????????????????????
            //        .AsImplementedInterfaces()  //????????
            //        .PropertiesAutowired()  //????????
            //        .SingleInstance();    //????????????????????

            //    containerBuilder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //    //??services??????????????Autofac??.
            //    containerBuilder.Populate(services);
            //    //????????.
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

            //????????????????????
            //app.UseConcurrencyLimiter();

            var jobOptions = new BackgroundJobServerOptions
            {
                Queues = new[] { "back", "front", "default" },//????????????????????
                WorkerCount = Environment.ProcessorCount * 1, //??????????
                ServerName = "conference hangfire1",//??????????
            };
            app.UseHangfireServer(jobOptions);
            //app.UseHangfireDashboard();

            ////??????????????????????????????
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyDashboardAuthorizationFilter() }
            });
            HangFireJob.AddOrUpdate();

            //app.UseMiddleware(typeof(ResponseHeaderMiddleware)); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseShardResource();
            app.UseMiddlewares();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });


            GlobalConfig.ServiceProvider = app.ApplicationServices;

        }
    }
}
