using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Framework.RabbitMQ
{
    public static class RabbitMQServiceCollectionExtensions
    {
        private static IServiceProvider ServiceProvider;

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services,Action<RabbitMQOptions> providerAction)        
        {
            services.AddOptions();
            services.Configure(providerAction);
            services.TryAddSingleton<IRabbitMQManager, RabbitMQManager>();    
            return services;
        }

        public static void UseRabbitMQ(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;             
            IRabbitMQManager rabbitMQManager = ServiceProvider.GetService<RabbitMQManager>()!;
            rabbitMQManager.Consume<string>("testQ", "testQ", x =>
            {
                Console.WriteLine($"recive : {x}");
            });
        }

    }
}
