using Framework.Hangfire;
using Hangfire;
using System;

namespace WebApi6_0.HangFire
{
    public class HangfireJobRunner : IHangfireJobRunner
    {
        public void Start()
        {

            RecurringJob.AddOrUpdate("HangFireTestId", () => Console.WriteLine($"{DateTime.Now}点钟执行 hangfire Recurring!"), Cron.Minutely, TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate<TestHangfireJob>(m => m.TestJob(), Cron.Minutely, TimeZoneInfo.Local); 
        }
    }
    public class TestHangfireJob
    { 
        private readonly IServiceProvider _provider; 
        /// <summary>
        /// 初始化一个<see cref="TestHangfireJob"/>类型的新实例
        /// </summary>
        public TestHangfireJob(IServiceProvider provider)
        { 
            _provider = provider;
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        public void TestJob()
        {
            Console.WriteLine("每分钟执行hangfire Recurring!");
        } 
    }
}
