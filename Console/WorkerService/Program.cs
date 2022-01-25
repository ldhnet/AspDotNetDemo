using WorkerService;
using WorkerService.Extensions;

IHost host = HostExtension.CreateHostBuilder(args).Build();

GlobalHostConfig.ServiceProvider=host.Services;

host.Services.UseRabbitMQProvider();

await host.RunAsync();
