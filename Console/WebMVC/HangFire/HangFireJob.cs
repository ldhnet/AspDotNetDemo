using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.HangFire
{
    public class HangFireJob
    {
        public static void AddOrUpdate()
        {
            RecurringJob.AddOrUpdate("HangFireTestId", () => Console.WriteLine("hangfire Recurring!"), Cron.Minutely(), TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("HangFireTestId2", () => Console.WriteLine("10点钟执行 hangfire Recurring!"), Cron.Daily(10), TimeZoneInfo.Local);
             
        }
    }
}
