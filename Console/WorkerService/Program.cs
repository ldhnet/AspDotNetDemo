using Framework.RabbitMQ;
using Framework.Utility.Config;
using NLog.Web;
using WorkerService;
using WorkerService.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() //使用windows服务
    .ConfigureServices((hostContext, services) =>
                    {
                        GlobalHostConfig.Services = services;
                        GlobalHostConfig.Configuration = hostContext.Configuration;
                        GlobalHostConfig.ConfigurationKeyValueList = hostContext.Configuration.AsEnumerable().ToList();
                        services.AddHostedServiceCollection();

                    })
    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Information);
                        logging.AddConsole();
                    })
    .UseNLog() 
    .Build();

GlobalHostConfig.ServiceProvider = host.Services; 
await host.RunAsync();

