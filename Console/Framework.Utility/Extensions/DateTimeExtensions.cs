using Framework.Utility.Models;

namespace Framework.Utility.Extensions
{
    /// <summary>
    /// 时间扩展操作类
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 当前时间是否周末
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            DayOfWeek[] weeks = { DayOfWeek.Saturday, DayOfWeek.Sunday };
            return weeks.Contains(dateTime.DayOfWeek);
        }

        /// <summary>
        /// 当前时间是否工作日
        /// </summary>
        /// <param name="dateTime">时间点</param>
        /// <returns></returns>
        public static bool IsWeekday(this DateTime dateTime)
        {
            DayOfWeek[] weeks = { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
            return weeks.Contains(dateTime.DayOfWeek);
        }

        /// <summary>
        ///  格式化日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Formatted(this DateTime? dateTime, string format = null)
        {
            return format != null
                ? (dateTime.HasValue ? dateTime.Value.ToString(format) : "")
                : (dateTime.HasValue ? dateTime.Value.ToString(FrameConstant.StringFormat.Date) : "");
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Formatted(this DateTime dateTime, string format = null)
        {
            return dateTime.ToString(format ?? FrameConstant.StringFormat.Date);
        }

        /// <summary>
        /// Get the first monday of the selected month
        /// </summary>
        /// <param name="selectedMonth"></param>
        /// <returns></returns>
        public static DateTime GetFirstMondayOfSelectedMonth(this DateTime selectedMonth)
        {
            var firstMonday = selectedMonth;
            while (firstMonday.DayOfWeek != DayOfWeek.Monday)
            {
                firstMonday = firstMonday.AddDays(1);
            }
            return firstMonday;
        }
        /// <summary>
        /// c# DateTime转Js时间戳 13位长整型
        /// </summary>
        /// <param name="dateTime">UTC时间</param>
        /// <returns></returns>
        public static long ToJsTimestamp(this DateTime dateTime)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long timeStamp = (long)(dateTime - startTime).TotalMilliseconds;
            return timeStamp;
        }

        /// <summary>
        /// DateTime转化为Local DateTime
        /// </summary>
        /// <param name="obj">日期类型值</param>
        /// <param name="offset">相对UTC时区偏移值,单位:分钟,例北京时间:-480</param>
        /// <param name="dateTimeFormat">日期格式</param>
        /// <returns></returns>
        public static string ToLocalTimeStr(this DateTime obj, int offset = 0, string dateTimeFormat = "MM/dd/yyyy HH:mm")
        {
            //指定obj为Utc时间
            var utcTime = DateTime.SpecifyKind(obj, DateTimeKind.Utc);
            var utcTimeOffset = new DateTimeOffset(obj, TimeSpan.Zero);

            var localTimeOffset = utcTimeOffset.ToOffset(new TimeSpan(0, -offset, 0));

            return localTimeOffset.ToString(dateTimeFormat);
        }

        /// <summary>
        /// JsTimestamp转化为Local DateTime
        /// </summary>
        /// <param name="obj">日期类型值</param>
        /// <param name="offset">相对UTC时区偏移值,单位:分钟,例北京时间:-480</param>
        /// <param name="dateTimeFormat">日期格式</param>
        /// <returns></returns>
        public static string ToLocalTimeStr(this long miliTime, int offset = 0, string dateTimeFormat = "MM/dd/yyyy HH:mm")
        {
            var utcBeginTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timeTicks = utcBeginTime.Ticks + miliTime * 10000;
            var date = new DateTime(timeTicks);
            return date.ToLocalTimeStr(offset, dateTimeFormat);
        }
    }
}