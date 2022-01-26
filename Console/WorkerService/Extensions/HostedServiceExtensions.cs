using Framework.Utility.Config;
using Framework.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Extensions
{ 
    public static class HostedServiceExtensions
    {
        public static void AddHostedServiceCollection(this IServiceCollection services)
        { 
            //services.AddRabbitMQ(option => option = GlobalContext.RabbitMQOptions);//RabbitMQ

            var serviceName= GlobalHostConfig.ConfigurationKeyValueList.FirstOrDefault(c =>c.Key == WebConstant.WorkerServiceName)!.Value;

            NLogHelper.Info($"serviceName ======{serviceName}");
             
            if (!string.IsNullOrEmpty(serviceName))
            {
                if (serviceName == WebConstant.WorkerServiceKey.HangfireService)
                {
                    services.AddHostedService<WorkerTwo>(); //添加WorkerTwo
                }
                if (serviceName == WebConstant.WorkerServiceKey.RabbitMQService)
                {
                    services.AddHostedService<Worker>(); //添加Worker
                }
            }
        }
    }
}
