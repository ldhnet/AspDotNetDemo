using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.Hangfire
{ 
    public class HangfireJobDemo : JobConfigAbstract, IJob
    {
        private readonly ILogger _logger;

        public HangfireJobDemo(ILogger<HangfireJobDemo> logger)
        {
            _logger = logger;
        }

        public override string JobId { get; set; } = "1";
        public override string JobName { get; set; } = "测试111";
        public override string CronExpression { get; set; } = "*/1 * * * * ?";
        public override string Queue { get; set; } = "default";
        public override bool Enable { get; set; } = false;

        /// <summary>
        /// 每分钟执行测试
        /// </summary>
        /// <returns></returns> 
        public async Task Execute()
        {
            //_logger.LogInformation("_logger HangfireJobDemo test start......");
            await Task.Run(() => Console.WriteLine($"HangfireJob_Demo {DateTime.Now}"));// {new Random().Next(100,999)} 
        }
    }
}
