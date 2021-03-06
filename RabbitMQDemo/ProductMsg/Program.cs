using Lib;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProductMsg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"ProductMsg client start....");
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

            string input;
            do
            {
                input = Console.ReadLine();

                manager.Publish<string>(input, "testQ", "testQ");
            }
            while (input.Trim().ToLower() != "q");

            Console.ReadLine();
        }        
    }
}
