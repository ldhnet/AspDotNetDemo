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
        #region 导出报表       
        /// <summary>
        /// 生成Excel Workbook 
        /// </summary>
        /// <param name="dataResources"></param>
        /// <returns></returns>
        public static IWorkbook DataToHSSFWorkbook(List<ExcelDataResource> dataResources)
        {
            HSSFWorkbook _Workbook = new HSSFWorkbook();

            #region 设置样式

            ICellStyle style1 = _Workbook.CreateCellStyle();//样式
            IFont font1 = _Workbook.CreateFont();//字体
            font1.FontName = "宋体";
            font1.FontHeightInPoints = 11;
            font1.IsBold = true;
            style1.SetFont(font1);//样式里的字体设置具体的字体样式

            #endregion

            #region Cell Format
             
            var  commonStyle = _Workbook.CreateCellStyle(); 
            SetCellStyle(commonStyle);
             
            //身份证，电话号码等
            var longNumStringStyle = _Workbook.CreateCellStyle();
            longNumStringStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("@");
            SetCellStyle(longNumStringStyle);

            var floatNumStyle = _Workbook.CreateCellStyle();
            floatNumStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("0.0");
            SetCellStyle(floatNumStyle);

            var decimalStyle = _Workbook.CreateCellStyle();
            decimalStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("0.00");
            SetCellStyle(decimalStyle);

            //日期
            var dateStyle = _Workbook.CreateCellStyle();
            dateStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("yyyy-mm-dd");
            SetCellStyle(dateStyle);

            //短时间
            var shortTimeStyle = _Workbook.CreateCellStyle();
            shortTimeStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("yyyy-mm-dd HH:MM");
            SetCellStyle(shortTimeStyle);

            //比率
            var zeroPercentStyle = _Workbook.CreateCellStyle();
            zeroPercentStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("0%");
            SetCellStyle(zeroPercentStyle);

            var nonZeroPercentStyle = _Workbook.CreateCellStyle();
            nonZeroPercentStyle.DataFormat = _Workbook.CreateDataFormat().GetFormat("0.00%");
            SetCellStyle(nonZeroPercentStyle);
            #endregion

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
                var patr = sheet.CreateDrawingPatriarch();

                object obj = sheetResource.SheetDataResource[0];
                Type type = obj.GetType();
                List<PropertyInfo> propList=type.GetProperties().Where(c=>c.IsDefined(typeof(ExportColumnAttribute),true)).ToList();

                int titleIndex = 0;
                if (sheetResource.TitleIndex >= 0)
                {
                    titleIndex = sheetResource.TitleIndex - 1;
                }

                IRow titleRow=sheet.CreateRow(titleIndex);                 
                titleRow.Height = 100 * 4; 
                var ColumnAttributes = GetDataColumnListByEntityType(propList);

                //表头
                for (int i = 0; i < propList.Count(); i++)
                {
                    ExportColumnAttribute propertyAttribute = propList[i].GetCustomAttribute<ExportColumnAttribute>();

                    ICell cell=titleRow.CreateCell(i);
                      
                    #region 设置描述
                    if (ColumnAttributes != null && ColumnAttributes.Any(c=>c.DataColumn.ColumnName == propList[i].Name && c.Comments.Length > 0))
                    {
                        var comments = ColumnAttributes.FirstOrDefault(c => c.DataColumn.ColumnName == propList[i].Name).Comments;
                        var comment1 = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 1, 0, i + 2, 5));
                        comment1.String = new HSSFRichTextString(comments);
                        comment1.Author = "Admin";
                        cell.CellComment = comment1;
                    }
                    #endregion

                    cell.SetCellValue(propertyAttribute.Title);         
                    SetHeaderCellStyle(_Workbook, cell); 

                    sheet.SetColumnWidth(i, 18 * 256);//设置列宽
                }
                //数据
                for (int i = 0; i < sheetResource.SheetDataResource.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1 + titleIndex);
                    object objInstance=sheetResource.SheetDataResource[i];
                    for (int j = 0; j < propList.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                         
                        var value = propList[j].GetValue(objInstance).ToString();
                        var format = ExcelCellFormat.StringFormat;
                        if (ColumnAttributes.FirstOrDefault(item => item.DataColumn.ColumnName == propList[j].Name) == null)
                        {
                            cell.SetCellValue(value);
                            cell.CellStyle= commonStyle;
                        }
                        else
                        {
                            format = ColumnAttributes.FirstOrDefault(item => item.DataColumn.ColumnName == propList[j].Name).CellFormat;
                        } 
                        #region 格式化数据
                        DateTime dateTimeResult;
                        switch (format)
                        {
                            case ExcelCellFormat.StringFormat:
                                cell.SetCellType(CellType.String);
                                cell.SetCellValue(value);
                                cell.CellStyle = commonStyle;
                                break;
                            case ExcelCellFormat.LongNumStringFormat:
                                cell.SetCellType(CellType.String);
                                cell.SetCellValue(value);
                                cell.CellStyle = longNumStringStyle;
                                break;
                            case ExcelCellFormat.DateFormat:
                                if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out dateTimeResult))
                                {
                                    cell.SetCellValue(dateTimeResult);
                                    cell.CellStyle = dateStyle;
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }
                                break;
                            case ExcelCellFormat.ShortTimeFormat:
                                if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out dateTimeResult))
                                {
                                    cell.SetCellValue(dateTimeResult);
                                    cell.CellStyle = shortTimeStyle;
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }
                                break;
                            case ExcelCellFormat.IntegerFormat:
                                int intResult;
                                if (int.TryParse(value, out intResult))
                                {
                                    cell.SetCellType(CellType.Numeric);
                                    cell.SetCellValue(intResult);
                                    cell.CellStyle = commonStyle;
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }

                                break;
                            case ExcelCellFormat.FloatFormat:
                                double doubleResult;
                                if (double.TryParse(value, out doubleResult))
                                {
                                    cell.SetCellValue(doubleResult);
                                    if (value.IndexOf(".", StringComparison.Ordinal) > 0)
                                        cell.CellStyle = floatNumStyle;
                                    else
                                        cell.CellStyle = commonStyle;
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }
                                break;
                            case ExcelCellFormat.DecimalFormat:
                                double d;
                                if (double.TryParse(value, out d))
                                {
                                    cell.SetCellValue(d);
                                    if (value.IndexOf(".", StringComparison.Ordinal) > 0)
                                        cell.CellStyle = decimalStyle;
                                    else
                                        cell.CellStyle = commonStyle;
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }
                                break;
                            case ExcelCellFormat.PercentageFormat:
                                value = propList[j].GetValue(objInstance).ToString().Replace("%", "");
                                decimal decimalResult;
                                if (decimal.TryParse(value, out decimalResult))
                                {
                                    if (decimalResult == 0m)
                                    {
                                        cell.SetCellValue((double)decimalResult);
                                        cell.CellStyle = zeroPercentStyle;
                                    }
                                    else
                                    {
                                        cell.SetCellValue((double)decimalResult / 100); // set value as number
                                        cell.CellStyle = nonZeroPercentStyle;
                                    }
                                }
                                else
                                {
                                    cell.SetCellValue(value);
                                    cell.CellStyle = commonStyle;
                                }
                                break;
                            default:
                                cell.SetCellType(CellType.String);
                                cell.SetCellValue(value);
                                cell.CellStyle = commonStyle;
                                break;
                        }
                        #endregion
                    }
                }
            }             
            return _Workbook;
        }

        /// <summary>
        /// 根据模板生成 excel Workbook
        /// </summary>
        /// <param name="list">导出的数据</param>
        /// <param name="startRowNum">最小起始行为3</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IWorkbook DataToHSSFWorkbook(List<object> list, string templateFile, int startRowNum = 1)
        {
            if (list.Count == 0) throw new Exception("数据为空");
            if (!File.Exists(templateFile))
                throw new Exception($"{templateFile}_导出模板不存在！");
            IWorkbook hssfWorkbook;
            using (FileStream file = new FileStream(templateFile, FileMode.Open))
            {
                hssfWorkbook = WorkbookFactory.Create(file);
            }
            var worksheet = hssfWorkbook.GetSheetAt(0);
            var type = list[0].GetType();
            List<PropertyInfo> propList = type.GetProperties().Where(c => c.IsDefined(typeof(ExportColumnAttribute), true)).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                IRow row = worksheet.CreateRow(i + startRowNum);
                object objInstance = list[i];
                for (int j = 0; j < propList.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    var value = propList[j].GetValue(objInstance).ToString();
                    cell.SetCellValue(value);
                }
            }
            return hssfWorkbook;
        }
        #endregion

        #region 导出报表
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

        /// <summary>
        /// stream 转化为DataTable
        /// </summary>
        /// <param name="hssfWorkbook"></param>
        /// <returns></returns>
        public static List<DataTable> ExcelStreamToDataTable(Stream stream)
        {
            IWorkbook hssfWorkbook = WorkbookFactory.Create(stream);
            return ToExcelDataTable(hssfWorkbook);
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 获得列的 Attribute
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        private static List<ExportDataColumn> GetDataColumnListByEntityType(List<PropertyInfo> entityProperties)
        {
            return (from prop in entityProperties
                    where prop.CustomAttributes.Any()
                    let attribute = prop.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(ExportColumnAttribute))
                    where attribute != null && attribute.NamedArguments.Count >= 2
                    select new ExportDataColumn
                    {
                        DataColumn = new DataColumn
                        {
                            ColumnName = prop.Name,
                            Caption = attribute.NamedArguments[0].TypedValue.Value.ToString(),
                        },
                        CellFormat =(ExcelCellFormat)Enum.Parse(typeof(ExcelCellFormat), attribute.NamedArguments[1].TypedValue.Value.ToString()),
                        Comments = attribute.NamedArguments.Count > 2 ? attribute.NamedArguments[2].TypedValue.Value.ToString() : string.Empty,
                    }).ToList();
        }

        /// <summary>
        /// 设置 Excel 表头样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        private static void SetHeaderCellStyle(HSSFWorkbook workbook, ICell cell)
        {
            var style = workbook.CreateCellStyle();
            var ffont = workbook.CreateFont();
            ffont.FontHeight = 13 * 15;
            //ffont.IsItalic = true; //斜体
            ffont.IsBold = true;
            ffont.FontName = "宋体";
            ffont.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.SetFont(ffont);

            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Aqua.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            style.Alignment = HorizontalAlignment.Center;//水平对齐            
            style.WrapText = false;//换行
            cell.CellStyle = style; 
        }
   
        /// <summary>
        /// 设置 Excel 内容样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        private static ICellStyle GetContentCellStyle(HSSFWorkbook workbook, ICell cell)
        {
            var style = workbook.CreateCellStyle();
            SetCellStyle(style);
            return style;
        }
 
        private static void SetCellStyle(ICellStyle style)
        {
            style.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            style.Alignment = HorizontalAlignment.Left;//水平对齐            
            style.WrapText = false;//换行 
        }
        #endregion 
    }
}
