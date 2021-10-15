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
        private static HangFireJobService hangFireJobService=new HangFireJobService();
        public static void AddOrUpdate()
        {
            RecurringJob.AddOrUpdate("HangFireTestId", () => hangFireJobService.PrintWriteLine(), Cron.Minutely(), TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("HangFireTestId2", () => Console.WriteLine("10点钟执行 hangfire Recurring!"), Cron.Daily(10), TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("HangFireTestId3", () => Console.WriteLine("0点钟执行 hangfire Recurring!"), Cron.Daily(), TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("HangFireTestId4", () => Console.WriteLine("1点钟执行 hangfire Recurring!"), Cron.Daily(1), TimeZoneInfo.Local);


            RecurringJob.AddOrUpdate("HangFireTestId4", () => Console.WriteLine("2点钟执行 hangfire Recurring!"), Cron.Daily(2), TimeZoneInfo.Local);



            BackgroundJob.Enqueue(() => Console.WriteLine("2点钟执行 hangfire Recurring!"));
        }
    }
}
