using WebApiB.Code;

namespace WebApiB.Code
{
    public class SendEmailService : BackgroundService, IDisposable
    {
        private readonly ISendEmailManager sendEmailManager;

        public SendEmailService(ISendEmailManager _sendEmailManager)
        {
            this.sendEmailManager = _sendEmailManager;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            //1 分钟刷新一次
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                sendEmailManager.SendMailUsingQueue();
            }
        }
 
    }
}
