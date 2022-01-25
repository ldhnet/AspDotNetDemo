using Framework.RabbitMQ;
using NLog.Web;
using WorkerService;
using WorkerService.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() //使用windows服务
    .ConfigureServices((hostContext, services) =>
                    {
                        GlobalHostConfig.Services = services;
                        GlobalHostConfig.Configuration = hostContext.Configuration;

                        //hostContext.Configuration.Bind("RabbitMQOptions", GlobalHostConfig.RabbitMQOptions);
                        //services.AddRabbitMQ(option => option = GlobalHostConfig.RabbitMQOptions);//RabbitMQ

                        services.AddHostedService<Worker>(); //添加Worker
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

