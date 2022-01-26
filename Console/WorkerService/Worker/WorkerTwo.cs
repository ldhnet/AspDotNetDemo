namespace WorkerService
{
    public class WorkerTwo : BackgroundService
    {
        private readonly ILogger<WorkerTwo> _logger;

        public WorkerTwo(ILogger<WorkerTwo> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("WorkerTwo 2222 running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}