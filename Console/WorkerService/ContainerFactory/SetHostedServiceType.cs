using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.RabbitMQ;
using Framework.Utility.Config;
using Framework.Utility.Extensions;

namespace WorkerService.ContainerFactory
{
    public class SetHostedServiceType
    { 
        private Dictionary<string, Action<string>> serviceTypeDic = new Dictionary<string, Action<string>>();
        private IServiceCollection services;
        public SetHostedServiceType()
        {
            DicInit();
            services = GlobalHostConfig.Services;
        } 
        public void DicInit()
        {
            serviceTypeDic.Add(WebConstant.WorkerServiceKey.HangfireService, f => HangfireService());
            serviceTypeDic.Add(WebConstant.WorkerServiceKey.RabbitMQService, f => RabbitMQService()); 
        }
        /// <summary>
        ///  设置启动服务
        /// </summary>
        /// <param name="serviceName">要启动的服务名称</param>
        public void SetHostedService(string serviceKey)
        { 
            Action<string> result = serviceTypeDic.Get(serviceKey);
            if (result != null)
            {
               result.Invoke(serviceKey);
            } 
        }

        private void HangfireService()
        {
            services.AddHostedService<WorkerTwo>(); 
        }
        private void RabbitMQService()
        {
            services.AddRabbitMQ(option => option = GlobalHostConfig.RabbitMQOptions);//RabbitMQ 
            //services.AddHostedService<Worker>(); 
        }
    }
}
