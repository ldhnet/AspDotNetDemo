using Microsoft.VisualBasic;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHLibrary
{
    public static class DateTableHelper
    {
        /// <summary>
        /// DataTable 转成 IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertDataTableToList2<T>(this DataTable dt)
        {
            var dict = new Dictionary<string, ImportedColumnAttribute>();

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
                    /*属性转换*/
                    var attributes = pro.GetCustomAttributes(typeof(ImportedColumnAttribute), true);
                    if (attributes == null || attributes.Length == 0) continue;

                    var attribute = attributes[0] as ImportedColumnAttribute;

                    if (columnNames.Contains(attribute?.Caption??string.Empty))
                        pro.SetValue(objT, row[attribute?.Caption ?? string.Empty]);

                    //if (columnNames.Contains(pro.Name))
                    //    pro.SetValue(objT, row[pro.Name]);
                }
                return objT;
            }).ToList();
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

        public static DataTable ReadExcel(string InputPath)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            try
            {
                using (var stream = new FileStream(InputPath, FileMode.Open))
                {
                    stream.Position = 0;
                    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                    sheet = xssWorkbook.GetSheetAt(0);
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        {
                            dtTable.Columns.Add(cell.ToString());
                        }
                    }
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                                {
                                    rowList.Add(row.GetCell(j).ToString());
                                }
                            }
                        }
                        if (rowList.Count > 0)
                            dtTable.Rows.Add(rowList.ToArray());
                        rowList.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReadExcel: {ex.Message}");
            }
            return dtTable;
            //return JsonConvert.SerializeObject(dtTable);
        }
    }
}
