using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataResources"></param>
        /// <returns></returns>
        public static IWorkbook DataToHSSFWorkbook(List<ExcelDataResource> dataResources)
        {
            HSSFWorkbook _Workbook = new HSSFWorkbook();

            if (dataResources == null || dataResources.Count == 0)
            {
                return _Workbook;
            }
            foreach (var sheetResource in dataResources)
            {
                if (sheetResource.SheetDataResource == null || sheetResource.SheetDataResource.Count == 0)
                {
                   break;
                }
                ISheet sheet= _Workbook.CreateSheet(sheetResource.SheetName);
                object obj = sheetResource.SheetDataResource[0];
                Type type = obj.GetType();
                List<PropertyInfo> propList=type.GetProperties().Where(c=>c.IsDefined(typeof(TitleAttribute),true)).ToList();

                int titleIndex = 0;
                if (sheetResource.TitleIndex >= 0)
                {
                    titleIndex = sheetResource.TitleIndex - 1;
                }

                IRow titleRow=sheet.CreateRow(titleIndex);

                ICellStyle style=_Workbook.CreateCellStyle();
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Aqua.Index;// 是设置前景色不是背景色
                style.FillPattern = FillPattern.SolidForeground;
                //style.FillBackgroundColor= NPOI.HSSF.Util.HSSFColor.Red.Index; 
                style.Alignment = HorizontalAlignment.CenterSelection;
                style.VerticalAlignment = VerticalAlignment.Center;
                titleRow.Height = 100 * 4;

                for (int i = 0; i < propList.Count(); i++)
                {
                    TitleAttribute propertyAttribute = propList[i].GetCustomAttribute<TitleAttribute>();
                    ICell cell=titleRow.CreateCell(i);
                    cell.SetCellValue(propertyAttribute.Title);
                    cell.CellStyle = style;
                }

                for (int i = 0; i < sheetResource.SheetDataResource.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1 + titleIndex);
                    object objInstance=sheetResource.SheetDataResource[i];
                    for (int j = 0; j < propList.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(propList[j].GetValue(objInstance).ToString());
                    }
                }
            }
             
            return _Workbook;
        }

        /// <summary>
        /// Excel 转化为DataTable
        /// </summary>
        /// <param name="hssfWorkbook"></param>
        /// <returns></returns>
        public static List<DataTable> ToExcelDataTable(IWorkbook hssfWorkbook)
        {
            List<DataTable> dataTables = new List<DataTable>();
            for (int sheetIndex = 0; sheetIndex < hssfWorkbook.NumberOfSheets; sheetIndex++)
            {
                ISheet sheet = hssfWorkbook.GetSheetAt(sheetIndex);
                IRow header=sheet.GetRow(sheet.FirstRowNum);
                if (header == null)
                {
                    break;
                }
                int startRow = 0;
                DataTable dtNpoi = new DataTable();
                startRow = sheet.FirstRowNum + 1;
                //表头
                for (int i = header.FirstCellNum; i < header.LastCellNum; i++)
                {
                    ICell cell = header.GetCell(i);
                    if (cell !=null)
                    {
                        string cellValue = $"Column{i+1}_{cell.ToString()}";
                        if (cellValue != null)
                        {
                            DataColumn col = new DataColumn(cellValue);
                            dtNpoi.Columns.Add(col);
                        }
                        else
                        {
                            DataColumn col = new DataColumn();
                            dtNpoi.Columns.Add(col);
                        }
                    }
                }
                //数据
                for (int i = startRow; i < sheet.LastRowNum; i++)
                {
                    IRow row=sheet.GetRow(i);//获取行
                    if (row == null)
                    {
                        continue;
                    }
                    DataRow dr=dtNpoi.NewRow();
                    //遍历每行的单元格
                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dr[j] = row.GetCell(j).ToString();

                        }
                    }
                    dtNpoi.Rows.Add(dr);
                }
                dataTables.Add(dtNpoi);
            } 
            return dataTables;
        }

        public static List<DataTable> ExcelStreamToDataTable(Stream stream)
        {
            IWorkbook hssfWorkbook = WorkbookFactory.Create(stream);
            return ToExcelDataTable(hssfWorkbook);
        }
    }
}
