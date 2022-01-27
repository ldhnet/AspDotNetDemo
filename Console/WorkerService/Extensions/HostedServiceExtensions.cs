using Framework.Utility.Config;
using Framework.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.ContainerFactory;

namespace WorkerService.Extensions
{ 
    public static class HostedServiceExtensions
    {
        public static void AddHostedServiceCollection(this IServiceCollection services)
        { 
            SetHostedServiceType serviceType =new SetHostedServiceType();
            var serviceName= GlobalHostConfig.ConfigurationKeyValueList.FirstOrDefault(c =>c.Key == WebConstant.WorkerServiceName)!.Value;
            serviceType.SetHostedService(serviceName);
            NLogHelper.Info($"serviceName ======{serviceName}"); 
        }

    }
}
