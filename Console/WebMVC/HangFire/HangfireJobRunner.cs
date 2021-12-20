using Framework.Hangfire;
using Hangfire;
using System;

namespace WebMVC.HangFire
{
    public class HangfireJobRunner : IHangfireJobRunner
    {
        public void Start()
        {  
            RecurringJob.AddOrUpdate<TestHangfireJob>(m => m.GetUserCount(), Cron.Minutely, TimeZoneInfo.Local); 
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
        public string GetUserCount()
        { 
            return "100";
        }
         
    }
}
