using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Utility.Extensions
{
    /// <summary>
    /// 强制安全转化辅助类(无异常抛出)
    /// </summary>
    public static class ConvertExtention
    {
        #region 强制安全转化
        /// <summary>
        /// object转化为Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            bool flag;
            if (obj == null)
            {
                return false;
            }

            if (obj.Equals(DBNull.Value))
            {
                return false;
            }
            return (bool.TryParse(obj.ToString(), out flag) && flag);
        }

        /// <summary>
        /// object强制转化为DateTime类型(吃掉异常)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="totheEndOftheDay">是否到当天的截止时间 23:59:59 也即是否包括当天</param>
        /// <returns></returns>
        public static DateTime? ToDateNull(this object obj, bool totheEndOftheDay = false)
        {
            if (obj == null)
            {
                return null;
            }
            try
            {
                return !totheEndOftheDay
                    ? new DateTime?(Convert.ToDateTime(obj))
                    : new DateTime?(Convert.ToDateTime(obj).Date.AddDays(1).AddMilliseconds(-3));
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// int强制转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            if (obj != null)
            {
                int num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (int.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }

        /// <summary>
        /// sbyte强制转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this bool obj)
        {
            var value = obj ? 1 : 0;
            return (sbyte)value;
        }

        public static sbyte ToSByte(this bool? obj)
        {
            if (!obj.HasValue)
                return 0;
            var value = obj.Value ? 1 : 0;
            return (sbyte)value;
        }
        /// <summary>
        /// 强制转化为long
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLong(this object obj)
        {
            if (obj != null)
            {
                long num;
                if (obj.Equals(DBNull.Value))
                {
                    return 0;
                }
                if (long.TryParse(obj.ToString(), out num))
                {
                    return num;
                }
            }
            return 0;
        }
        /// <summary>
        /// 强制转化可空int类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToIntNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new int?(obj.ToInt());
        }
        /// <summary>
        /// 强制转化为string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SafeToStr(this object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (obj.Equals(DBNull.Value))
            {
                return "";
            }
            return Convert.ToString(obj);
        }

        /// <summary>
        /// Decimal转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj)
        {
            return obj.ToDecimal(0M);
        }
        /// <summary>
        /// Decimal转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultVal)
        {
            if (obj == null)
            {
                return defaultVal;
            }
            if (obj.Equals(DBNull.Value))
            {
                return defaultVal;
            }
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// Decimal可空类型转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNull(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj.Equals(DBNull.Value))
            {
                return null;
            }
            return new decimal?(obj.ToDecimal());
        }

        /// <summary>
        /// Double转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            if (obj == null)
            {
                return 0D;
            }
            if (obj.Equals(DBNull.Value))
            {
                return 0D;
            }
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0D;
            }
        }

        /// <summary>
        /// 将多值查询字段例如"1,2,3"转为int List [1,2,3]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IList<int> StringToList(this string str)
        {
            return str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.ToInt()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateRange"></param>
        /// <returns></returns>
        //public static DateRange StringToDateRange(this string dateRange)
        //{
        //    if (string.IsNullOrEmpty(dateRange) || dateRange.IndexOf("-", StringComparison.Ordinal) < 0)
        //        //return new DateRange {BeginTime = DateTime.MinValue, EndTime = DateTime.MaxValue};
        //        return null;
        //    return new DateRange
        //    {
        //        BeginTime = Convert.ToDateTime(dateRange.Substring(0, dateRange.IndexOf("-", StringComparison.Ordinal))),
        //        EndTime =
        //            Convert.ToDateTime(dateRange.Substring(dateRange.IndexOf("-", StringComparison.Ordinal) + 1))
        //                .AddSeconds(60 * 60 * 24 - 1)
        //    };
        //}

        /// <summary>
        /// 取字典键值
        /// </summary>
        /// <param name="dict">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetKey<TKey>(this Dictionary<TKey, string> dict, TKey key, string defaultValue = "")
        {
            if (dict.ContainsKey(key))
                return dict[key];

            return defaultValue;
        }
        #endregion
    }
}
