namespace Lee.Hangfire
{
    public abstract class JobConfigAbstract
    {
        public abstract string JobId { get; set; }
        public abstract string JobName { get; set; }
        public abstract string CronExpression { get; set; }
        public abstract string Queue { get; set; }
        public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Local;
        public abstract bool Enable { get; set; }
    }
}