using Framework.RabbitMQ;
using NLog.Web;
using WorkerService;
using WorkerService.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() //ʹ��windows����
    .ConfigureServices((hostContext, services) =>
                    {
                        GlobalHostConfig.Services = services;
                        GlobalHostConfig.Configuration = hostContext.Configuration;

                        //hostContext.Configuration.Bind("RabbitMQOptions", GlobalHostConfig.RabbitMQOptions);
                        //services.AddRabbitMQ(option => option = GlobalHostConfig.RabbitMQOptions);//RabbitMQ

                        services.AddHostedService<Worker>(); //���Worker
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

//host.Services.UseRabbitMQProvider();

await host.RunAsync();

