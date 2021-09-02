using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole.Interface
{
    public partial class PlanningTaskInterface: IPlanningTaskInterface
    {
        /// <summary>
        /// 任务委托
        /// </summary>
        /// <param name="param"></param>
        public delegate void TaskHandler(DateTime param, bool isfirsttime = false);
 
        /// <summary>
        /// 任务开始入口
        /// </summary>
        public void Do()
        {
            //任务1  每天，0：00开始
            PerformTasks("任务1",  Task1, TimeOfDay.HourAndMinuteOfDay(0, 0));
            //任务2 实时
            PerformTasks("任务2", Task2);

            //每月1日  任务2
            PerformTasks("任务3", Task3,  DayOfMonth.DayOfMonthByTypeAndDay(DayOfMonthType.FromBeginning, 1),TimeOfDay.HourAndMinuteOfDay(2, 0));
        }
        /// <summary>
        /// Timely
        /// </summary>
        /// <param name="taskname"></param>
        /// <param name="callback"></param>
        private void PerformTasks(string taskname, TaskHandler callback)
        {
            PerformTasks(taskname, callback, null);
        }

        /// <summary>
        /// Daily
        /// </summary>
        /// <param name="taskname"></param>
        /// <param name="callback"></param>
        /// <param name="timeOfDay"></param>
        private void PerformTasks(string taskname, TaskHandler callback, TimeOfDay timeOfDay)
        {
            PerformTasks(taskname, callback, null, timeOfDay);
        }

        /// <summary>
        /// Monthly
        /// </summary>
        /// <param name="taskname"></param>
        /// <param name="callback"></param>
        /// <param name="dayOfMonth"></param>
        /// <param name="timeOfDay"></param>
        private void PerformTasks(string taskname, TaskHandler callback, DayOfMonth dayOfMonth, TimeOfDay timeOfDay)
        {
            //获取执行时间
            var execDate = DateTime.Now;

            try
            {
                //如果同时没有传递dayOfMonth和timeOfDay参数，则任务类型为 Timely，不用记录到数据库日志
                if (dayOfMonth == null && timeOfDay == null)
                {
                    callback(execDate);
                    //写入成功操作日志
                    TaskLog($"自动执行任务[{taskname}]，任务日期{execDate.ToShortDateString()}");
                    return;
                }

                //如果dayOfMonth不为空，timeOfDay为空，则置timeOfDay为默认1：00，任务类型为 Monthly
                if (dayOfMonth != null && timeOfDay == null)
                    timeOfDay = TimeOfDay.HourAndMinuteOfDay(1, 0);
                //获取最后一次执行的日期
                var lastTaskDay = DateTime.Now.AddDays(-1);

                //如果最后一次执行的日期为空，为系统第一次运行
                if (lastTaskDay == null)
                {
                    if (timeOfDay == null)
                        timeOfDay = TimeOfDay.HourAndMinuteOfDay(1, 0);
                    if (timeOfDay.Before(TimeOfDay.HourAndMinuteOfDay(execDate.Hour, execDate.Minute)))
                        if (dayOfMonth == null || dayOfMonth.Compare(execDate) || dayOfMonth.Before(execDate))
                        {
                            //TODO 进入当天任务
                            //UpdateRecord(taskname, execDate, false);
                            callback(execDate, true);
                        }
                        else
                            return;
                }
                else
                {
                    if (dayOfMonth == null)
                    {
                        //如果日志中间发现有任务被间断，则执行遗漏的日期中的第一天
                        if ((execDate.Date - lastTaskDay.Date).Days > 1)
                        {
                            //设置执行日期为遗漏的第一天
                            execDate = lastTaskDay.AddDays(1);
                            //UpdateRecord(taskname, execDate, false);
                            //执行回调方法
                            callback(execDate);
                            //UpdateRecord(taskname, execDate, true);
                            //写入操作记录
                            //AddRecord(taskname, execDate);

                            //递归执行
                            PerformTasks(taskname, callback, null, timeOfDay);

                            return;
                        }
                        if ((execDate.Date - lastTaskDay.Date).Days == 1)
                        {
                            if (timeOfDay.Before(TimeOfDay.HourAndMinuteOfDay(execDate.Hour, execDate.Minute)))
                            {
                                //UpdateRecord(taskname, execDate, false);
                                //TODO 进入当天任务
                                callback(execDate);
                            }
                            else
                                return;
                        }
                        else
                            return;
                    }
                    else
                    {
                        //计算当前执行日期和最后月份差
                        var month = (execDate.Year - lastTaskDay.Year) * 12 + (execDate.Month - lastTaskDay.Month);

                        if (month > 1)
                        {
                            //中间有任务被间断，执行遗漏的月份中的第一个月
                            execDate = lastTaskDay.AddMonths(1);

                            //UpdateRecord(taskname, execDate);

                            //执行回调方法
                            callback(execDate);
                            //UpdateRecord(taskname, execDate, true);

                            //写入操作记录
                            //AddRecord(taskname, execDate);

                            //递归执行
                            PerformTasks(taskname, callback, dayOfMonth, timeOfDay);

                            return;
                        }
                        if (month == 1)
                        {
                            if (timeOfDay.Before(TimeOfDay.HourAndMinuteOfDay(execDate.Hour, execDate.Minute)))
                            {
                                if (dayOfMonth.Compare(execDate) || dayOfMonth.Before(execDate))
                                {
                                    //UpdateRecord(taskname, execDate, false);

                                    //TODO 进入当天任务
                                    callback(execDate);
                                }
                                else
                                    return;
                            }
                            else
                                return;
                        }
                        else
                            return;
                    }
                }
                //写入操作记录
                //UpdateRecord(taskname, execDate, true);
            }
            catch (Exception ex)
            {
                TaskLog($"自动执行任务[{taskname}] 失败，任务日期{execDate.ToShortDateString()}，错误原因：{ex.StackTrace}");
            }
        }

        private void TaskLog(string parm)
        {
            StreamWriter sW = new StreamWriter(@"c:\temp\tasklog.txt", true, Encoding.UTF8);
            sW.WriteLine(parm);
            sW.Close();
        }



        /// <summary>
        /// Represents a time in hour, minute and second of any given day.
        /// <remarks>
        /// <para>
        /// The hour is in 24-hour convention, meaning values are from 0 to 23.
        /// </para>
        /// </remarks>
        /// </summary>
        public class TimeOfDay
        {
            private readonly int hour;
            private readonly int minute;
            private readonly int second;

            /// <summary>
            /// Create a TimeOfDay instance for the given hour, minute and second.
            /// </summary>
            /// <param name="hour">The hour of day, between 0 and 23.</param>
            /// <param name="minute">The minute of the hour, between 0 and 59.</param>
            /// <param name="second">The second of the minute, between 0 and 59.</param>
            public TimeOfDay(int hour, int minute, int second)
            {
                this.hour = hour;
                this.minute = minute;
                this.second = second;
                Validate();
            }

            /// <summary>
            /// Create a TimeOfDay instance for the given hour, minute (at the zero second of the minute).
            /// </summary>
            /// <param name="hour">The hour of day, between 0 and 23.</param>
            /// <param name="minute">The minute of the hour, between 0 and 59.</param>
            public TimeOfDay(int hour, int minute)
            {
                this.hour = hour;
                this.minute = minute;
                second = 0;
                Validate();
            }

            private void Validate()
            {
                if (hour < 0 || hour > 23)
                {
                    throw new ArgumentException("Hour must be from 0 to 23");
                }

                if (minute < 0 || minute > 59)
                {
                    throw new ArgumentException("Minute must be from 0 to 59");
                }

                if (second < 0 || second > 59)
                {
                    throw new ArgumentException("Second must be from 0 to 59");
                }
            }

            /// <summary>
            /// Create a TimeOfDay instance for the given hour, minute and second.
            /// </summary>
            /// <param name="hour">The hour of day, between 0 and 23.</param>
            /// <param name="minute">The minute of the hour, between 0 and 59.</param>
            /// <param name="second">The second of the minute, between 0 and 59.</param>
            /// <returns></returns>
            public static TimeOfDay HourMinuteAndSecondOfDay(int hour, int minute, int second)
            {
                return new TimeOfDay(hour, minute, second);
            }

            /// <summary>
            /// Create a TimeOfDay instance for the given hour, minute (at the zero second of the minute)..
            /// </summary>
            /// <param name="hour">The hour of day, between 0 and 23.</param>
            /// <param name="minute">The minute of the hour, between 0 and 59.</param>
            /// <returns>The newly instantiated TimeOfDay</returns>
            public static TimeOfDay HourAndMinuteOfDay(int hour, int minute)
            {
                return new TimeOfDay(hour, minute);
            }

            /// <summary>
            /// The hour of the day (between 0 and 23).
            /// </summary>
            public int Hour
            {
                get { return hour; }
            }

            /// <summary>
            /// The minute of the hour (between 0 and 59).
            /// </summary>
            public int Minute
            {
                get { return minute; }
            }

            /// <summary>
            /// The second of the minute (between 0 and 59).
            /// </summary>
            public int Second
            {
                get { return second; }
            }

            /// <summary>
            /// Determine with this time of day is before the given time of day.
            /// </summary>
            /// <param name="timeOfDay"></param>
            /// <returns>True this time of day is before the given time of day.</returns>
            public bool Before(TimeOfDay timeOfDay)
            {
                if (timeOfDay.hour != hour)
                    return timeOfDay.hour > hour;
  
                if (timeOfDay.minute != minute)
                    return timeOfDay.minute > minute;

                if (timeOfDay.second != second)
                    return timeOfDay.second > second;

                return false; // must be equal...
            }

            public override bool Equals(Object obj)
            {
                if (!(obj is TimeOfDay))
                {
                    return false;
                }

                var other = (TimeOfDay)obj;

                return (other.hour == hour && other.minute == minute && other.second == second);
            }

            public override int GetHashCode()
            {
                return (hour + 1) ^ (minute + 1) ^ (second + 1);
            }

            /// <summary>
            /// Return a date with time of day reset to this object values. The millisecond value will be zero.
            /// </summary>
            /// <param name="dateTime"></param>
            public DateTimeOffset? GetTimeOfDayForDate(DateTimeOffset? dateTime)
            {
                if (dateTime == null)
                {
                    return null;
                }

                var cal = new DateTimeOffset(dateTime.Value.Date, dateTime.Value.Offset);
                var t = new TimeSpan(0, hour, minute, second);
                return cal.Add(t);
            }

            public override string ToString()
            {
                return "TimeOfDay[" + hour + ":" + minute + ":" + second + "]";
            }
        }

        /// <summary>
        /// Type enum
        /// </summary>
        public enum DayOfMonthType
        {
            FromBeginning,
            FromEnd
        }

        /// <summary>
        /// Day-of-Month can be any value 0-31, but you need to be careful about how many days are in a given month!
        /// </summary>
        public class DayOfMonth
        {
            private readonly DayOfMonthType type;
            private int day;

            public DayOfMonth(int day)
            {
                type = DayOfMonthType.FromBeginning;
                this.day = day <= 0 ? 1 : day;
            }

            public DayOfMonth(DayOfMonthType type, int day)
            {
                this.type = type;
                this.day = day <= 0 ? 1 : day;
            }

            public static DayOfMonth DayOfMonthByTypeAndDay(int _day)
            {
                return new DayOfMonth(_day);
            }

            public static DayOfMonth DayOfMonthByTypeAndDay(DayOfMonthType _type, int _day)
            {
                return new DayOfMonth(_type, _day);
            }

            public bool Compare(DateTime other)
            {
                //当月的天数
                var daysOfMonth = DateTime.DaysInMonth(other.Year, other.Month);

                if (day > daysOfMonth)
                    day = daysOfMonth;

                switch (type)
                {
                    case DayOfMonthType.FromEnd:
                        return (daysOfMonth - day + 1) == other.Day;

                    default:
                        return other.Day == day;
                }
            }

            public override bool Equals(Object obj)
            {
                if (!(obj is DateTime))
                {
                    return false;
                }
                var other = (DateTime)obj;

                //当月的天数
                var daysOfMonth = DateTime.DaysInMonth(other.Year, other.Month);

                if (day > daysOfMonth)
                    day = daysOfMonth;

                switch (type)
                {
                    case DayOfMonthType.FromEnd:
                        return (daysOfMonth - day + 1) == other.Day;

                    default:
                        return other.Day == day;
                }
            }

            public override int GetHashCode()
            {
                return ((int)type + 1) ^ (day + 1);
            }

            /// <summary>
            /// 仅比较传入参数的月份
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public bool Before(DateTime date)
            {
                //当月的天数
                var daysOfMonth = DateTime.DaysInMonth(date.Year, date.Month);

                switch (type)
                {
                    case DayOfMonthType.FromEnd:
                        return (daysOfMonth - day + 1) < date.Day;

                    default:
                        return day < date.Day;
                }
            }

            public int Day
            {
                get { return day; }
            }

            public DayOfMonthType? Type
            {
                get { return type; }
            }
        }

    }
}
