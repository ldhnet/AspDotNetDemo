using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDBTest
{
    public static class ExtensionHelper
    {
        public static string ExtToString<T>(this T t)
        {
            return t.ToString();
        }
        /// <summary>
        /// DataTable 转成 IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToList<T>(this DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                   .Select(c => c.ColumnName)
                   .ToList();

            var properties = typeof(T).GetProperties();
            DataRow[] rows = dt.Select();
            return rows.Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                        pro.SetValue(objT, row[pro.Name]);
                }
                return objT;
            }).ToList();
        }
        /// <summary>
        /// list 转 DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> array)
        {
            DataTable result = new DataTable();
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
            {
                if (pd.PropertyType.IsGenericType && pd.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    result.Columns.Add(pd.Name, Nullable.GetUnderlyingType(pd.PropertyType));
                else
                    result.Columns.Add(pd.Name, pd.PropertyType);
            }
            foreach (T item in array)
            {
                DataRow row = result.NewRow();
                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(typeof(T)))
                    row[pd.Name] = pd.GetValue(item) ?? DBNull.Value;
                result.Rows.Add(row);
            }
            return result;
        }
    }
}
