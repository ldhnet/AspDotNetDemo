using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Utility.Reflection
{
    /// <summary>
    /// 反射辅助操作类
    /// </summary>
    public class ReflectionHelper
    {
        private static ConcurrentDictionary<string, object> dictCache = new ConcurrentDictionary<string, object>();

        #region 得到类里面的属性集合
        /// <summary>
        /// 得到类里面的属性集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties(Type type, string[] columns = null)
        {
            PropertyInfo[] properties = null;
            if (dictCache.ContainsKey(type.FullName))
            {
                properties = dictCache[type.FullName] as PropertyInfo[];
            }
            else
            {
                properties = type.GetProperties();
                dictCache.TryAdd(type.FullName, properties);
            }

            if (columns != null && columns.Length > 0)
            {
                //  按columns顺序返回属性
                var columnPropertyList = new List<PropertyInfo>();
                foreach (var column in columns)
                {
                    var columnProperty = properties.Where(p => p.Name == column).FirstOrDefault();
                    if (columnProperty != null)
                    {
                        columnPropertyList.Add(columnProperty);
                    }
                }
                return columnPropertyList.ToArray();
            }
            else
            {
                return properties;
            }
        }
        #endregion

        /// <summary>
        /// 反射获取对象属性值
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="t">获取属性值的对象</param>
        /// <param name="propertyname">属性名</param>
        /// <returns>属性值</returns>
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }
    }
}
