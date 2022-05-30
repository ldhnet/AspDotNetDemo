using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace Lee.Hangfire
{
    public static class HangfireExtension
    {
        public static void AddHangfire(this IServiceCollection services, ConfigurationManager configuration)
        {
     
            var connstring = JobServer.GetRedisStorageOptions(configuration);
            services.AddHangfire(_configuration =>
            { 
                //持久化到Redis
                _configuration.UseRedisStorage(JobServer.GetRedisStorageOptions(configuration), new RedisStorageOptions()
                {
                    Db = 3,
                    Prefix = "DH3_"
                });
            });
            BackgroundJobServerOptions optionsAction = JobServer.GetBackgroundJobServerOptions(configuration)!;
            services.AddHangfireServer(o => o = optionsAction);

            services.AddSingleton<IJobService, JobService>();
            
            services.AddHangfireSingleton();
        }

        public static IServiceCollection AddHangfireSingleton(this IServiceCollection services)
        {
            var jobs = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(t => t.GetExportedTypes())
                .Where(x => x.IsClass && typeof(IJob).IsAssignableFrom(x));
            foreach (var job in jobs)
            {
                services.AddSingleton(typeof(IJob), job);
            }
            return services;
        }


        public static void UseHangfire(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>()!;
            string url = configuration["Hangfire:DashboardUrl"].ToString();
            DashboardOptions dashboardOptions = JobServer.GetDashboardOptions(configuration);
            app.UseHangfireDashboard(url, dashboardOptions);

            var jobService = app.ApplicationServices.GetService<IJobService>()!;
            jobService.EnqueueTasks();
        }

    }

    public class JobServer
    {
        /// <summary>
        /// 获取后台作业服务器选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public static string GetRedisStorageOptions(IConfiguration configuration)
        {
            return configuration["Hangfire:StorageConnectionString"].ToString();
        }

        /// <summary>
        /// 获取后台作业服务器选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public static BackgroundJobServerOptions GetBackgroundJobServerOptions(IConfiguration configuration)
        {
            BackgroundJobServerOptions serverOptions = new BackgroundJobServerOptions()
            {
                Queues = new[] { "testlee", "default" },//队列名称，只能为小写
                //WorkerCount = Environment.ProcessorCount * 1, 
                ServerName = "conference hangfire demo",//服务器名称
            };

            int workerCount = Convert.ToInt32(configuration["Hangfire:WorkerCount"]);
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
            string[] roles = configuration["Hangfire:Roles"].ToString().Split(""); //new string[] { "admin" };//
            DashboardOptions dashboardOptions = new DashboardOptions();
            //限制角色存在时，才启用角色限制
            if (roles.Length > 0)
            {
               // dashboardOptions.Authorization = new[] { new RoleDashboardAuthorizationFilter(roles) };
            }
            return dashboardOptions;
        }

    }
}
