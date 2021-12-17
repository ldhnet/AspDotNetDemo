using MyWorkerService; 
IHost host = HostExtension.CreateHostBuilder(args).Build();
await host.RunAsync();