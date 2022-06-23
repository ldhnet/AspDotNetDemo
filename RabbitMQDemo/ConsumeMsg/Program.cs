using Lib;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsumeMsg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"ConsumeMsg client start....");
            var services = new ServiceCollection();            
            services.AddRabbitMQ(x => 
            {
                x.HostName = "localhost";
                x.Port = 5672;
                x.UserName = "guest";
                x.Password = "guest";
                x.ExchangeName = "TestExchange";                
            });            

            var serviceProvider = services.BuildServiceProvider();

            var manager = serviceProvider.GetService<IRabbitMQManager>();

            manager.Consume<string>("testQ", "testQ", x => 
            {
                Console.WriteLine($"recive : {x}");
            });          
        }
    }
}
