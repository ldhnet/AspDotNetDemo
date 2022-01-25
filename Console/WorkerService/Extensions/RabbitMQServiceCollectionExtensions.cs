using Framework.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using WorkerService.ContainerFactory;

namespace WorkerService.Extensions
{
    public static class RabbitMQServiceCollectionExtensions
    { 
        public static void UseRabbitMQProvider(this IServiceProvider app)
        { 
            IRabbitMQManager mqManager = app.GetService<IRabbitMQManager>()!;
            mqManager.Consume<string>("testQ", "testQ", x =>
            {
                Console.WriteLine($"recive : {x}");
            });
        }
    } 
}
