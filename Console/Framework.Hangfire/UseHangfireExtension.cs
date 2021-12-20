using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Framework.Utility.Exceptions;
using Framework.Utility.Extensions;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Hangfire
{
    public static  class UseHangfireExtension
    { 
        public static void UseHangfire(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>()!;
 
            //BackgroundJobServerOptions serverOptions = new JobServer().GetBackgroundJobServerOptions(configuration);
            //app.UseHangfireServer(serverOptions);

            //string url = configuration["OSharp:Hangfire:DashboardUrl"].CastTo("/hangfire");
            //DashboardOptions dashboardOptions = new JobServer().GetDashboardOptions(configuration);
            //app.UseHangfireDashboard(url, dashboardOptions);

            IHangfireJobRunner jobRunner = serviceProvider.GetService<IHangfireJobRunner>()!;
            jobRunner?.Start(); 
        }

    }

    public class JobServer
    {
        /// <summary>
        /// 获取后台作业服务器选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public BackgroundJobServerOptions GetBackgroundJobServerOptions(IConfiguration configuration)
        {
            BackgroundJobServerOptions serverOptions = new BackgroundJobServerOptions();
            int workerCount = configuration["OSharp:Hangfire:WorkerCount"].CastTo(0);
            if (workerCount > 0)
            {
                serverOptions.WorkerCount = workerCount;
            }
            return serverOptions;
        }

        /// <summary>
        /// 获取Hangfire仪表盘选项
        /// </summary>
        /// <param name="configuration">系统配置信息</param>
        /// <returns></returns>
        public DashboardOptions GetDashboardOptions(IConfiguration configuration)
        {
            string[] roles = configuration["OSharp:Hangfire:Roles"].CastTo("").Split(",", true);
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
