using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Framework.Utility.Exceptions; 
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire.Redis;

namespace Framework.Hangfire
{
    public static  class UseHangfireExtension
    {
        public static void AddHangfire(this IServiceCollection services,ConfigurationManager configuration)
        {
            //services.AddHangfire(r => r.UseSqlServerStorage(JobServer.GetSqlServerStorageOptions(configuration))); //持久化到数据库

            var prefix = new HangfirePrefix("app_Name");
       
            var connstring = JobServer.GetSqlServerStorageOptions(configuration);
            services.AddHangfire(_configuration =>
            { 
                //_configuration.UseRedisStorage(connstring);
                //持久化到Redis
                _configuration.UseRedisStorage(JobServer.GetSqlServerStorageOptions(configuration), new RedisStorageOptions()
                {
                    Db = 0,
                    Prefix = prefix.GetHangfirePrefix()
                });
            });
            BackgroundJobServerOptions optionsAction = JobServer.GetBackgroundJobServerOptions(configuration)!;
            services.AddHangfireServer(o=> o = optionsAction );
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>()!;
             
            string url = configuration["Hangfire:DashboardUrl"].CastTo("/hangfire");
            DashboardOptions dashboardOptions = JobServer.GetDashboardOptions(configuration);
            app.UseHangfireDashboard(url, dashboardOptions);

            IHangfireJobRunner jobRunner = serviceProvider.GetService<IHangfireJobRunner>()!;
            jobRunner?.Start(); 
        }

    }

    public  class JobServer
    {
        /// <summary>
        /// 获取后台作业服务器选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public static string GetSqlServerStorageOptions(IConfiguration configuration)
        {
            return configuration["Hangfire:StorageConnectionString"].CastTo(""); 
        }

        /// <summary>
        /// 获取后台作业服务器选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public static BackgroundJobServerOptions GetBackgroundJobServerOptions(IConfiguration configuration)
        {
            BackgroundJobServerOptions serverOptions = new BackgroundJobServerOptions() {
                Queues = new[] { "TestLee", "default" },//队列名称，只能为小写
                //WorkerCount = Environment.ProcessorCount * 1, 
                ServerName = "conference hangfire1",//服务器名称
            };
               
            int workerCount = configuration["Hangfire:WorkerCount"].CastTo(0);
            if (workerCount > 0)
            {
                serverOptions.WorkerCount = workerCount; //并发任务数
            }
            return serverOptions;
        }

        /// <summary>
        /// 获取Hangfire仪表盘选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public static DashboardOptions GetDashboardOptions(IConfiguration configuration)
        {
            string[] roles =configuration["Hangfire:Roles"].CastTo("").Split(""); //new string[] { "admin" };//
            DashboardOptions dashboardOptions = new DashboardOptions();
            //限制角色存在时，才启用角色限制
            if (roles.Length > 0)
            {
                dashboardOptions.Authorization = new[] { new RoleDashboardAuthorizationFilter(roles) };
            }
            return dashboardOptions;
        }

    }
}
