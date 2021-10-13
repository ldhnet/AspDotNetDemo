using System;

namespace WebMVC.HangFire
{
    public class HangFireJobService
    {
        public void PrintWriteLine()
        {
            Console.WriteLine("每分钟执行hangfire Recurring!");
        }
    }
}
