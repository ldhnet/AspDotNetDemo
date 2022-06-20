using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Hangfire
{ 
    public class HangfireJobTest : JobConfigAbstract, IJob
    {
        private readonly ILogger _logger;

        public HangfireJobTest(ILogger<HangfireJobTest> logger)
        {
            _logger = logger;
        }

        public override string JobId { get; set; } = "2";
        public override string JobName { get; set; } = "测试222";
        public override string CronExpression { get; set; } = Cron.Minutely();
        public override string Queue { get; set; } = "default";
        public override bool Enable { get; set; } = true;

        /// <summary>
        /// 每分钟执行测试
        /// </summary>
        /// <returns></returns> 
        public async Task Execute()
        {
            //_logger.LogInformation("_logger HangfireJobDemo test false start......");
            await Task.Run(() => Console.WriteLine($"HangfireJob_Test {DateTime.Now}"));
        }
    }
}
