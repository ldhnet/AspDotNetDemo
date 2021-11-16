using NPOI.HSSF.UserModel;
//using Excel = Microsoft.Office.Interop.Excel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebMVC.Helper
{
    public enum ExcelCellColor { SKY_BLUE = 40, ORANGE = 53, LIGHT_GREEN = 42, PINK = 14 }

    public class ExcelBuilder
    {
        public static void CreateExcel(DataTable DTDataSource, string fileName, string filePath)
        {
            //Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            //Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks.Add(Missing.Value);

            //xApp.DisplayAlerts = false;
            //Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];
            //if (System.IO.Directory.Exists(filePath))
            //{
            //	throw new Exception("");
            //}

            //string[,] data = new string[DTDataSource.Rows.Count, DTDataSource.Columns.Count];

            //for (int i = 0; i < DTDataSource.Rows.Count; i++)
            //{
            //	for (int j = 0; j < DTDataSource.Columns.Count; j++)
            //	{
            //		data[i, j] = DTDataSource.Rows[i][j].ToString();
            //	}
            //}

            //Microsoft.Office.Interop.Excel.Range range = xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[DTDataSource.Rows.Count, DTDataSource.Columns.Count]);
            //range.Value2 = data;

            //xBook.SaveAs(Path.Combine(filePath, fileName), Missing.Value, Missing.Value, Missing.Value,
            //	Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
            //	Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //xBook.Close(Missing.Value, Missing.Value, Missing.Value);

            //xApp.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(xApp);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(xSheet);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(xBook);
            //xSheet = null;
            //xBook = null;
            //xApp = null;
            //System.GC.Collect(0);
        }

        public static byte[] ExportExcelBytes<T>(List<T> listSource, Dictionary<string, string> columnNameDic)
        {
            byte[] bytes = null;

            if (columnNameDic == null || columnNameDic.Count == 0)
                throw (new Exception("Pls set column name of exporting excel!"));

            HSSFWorkbook excelWorkbook = CreateExcelFile();

            InsertRow(listSource, excelWorkbook, columnNameDic);

            using (MemoryStream ms = new MemoryStream())
            {
                excelWorkbook.Write(ms);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        public static byte[] ExportMergedHeaderExcelBytes<T>(
            List<T> listSource,
            Dictionary<string, Dictionary<string, string>> columnNameDic)
        {
            byte[] bytes = null;

            if (columnNameDic == null || columnNameDic.Count == 0)
                throw (new Exception("Pls set column name of exporting excel!"));

            HSSFWorkbook excelWorkbook = CreateExcelFile();

            InsertMergedHeaderRow(listSource, excelWorkbook, columnNameDic);

            using (MemoryStream ms = new MemoryStream())
            {
                excelWorkbook.Write(ms);
                bytes = ms.ToArray();
            }
            return bytes;
        }

        private static HSSFWorkbook CreateExcelFile()
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            return hssfworkbook;
        }

        private static void InsertRow<T>(List<T> listSource, HSSFWorkbook excelWorkbook, Dictionary<string, string> columnNameDic)
        {
            int rowCount = 0;
            int sheetCount = 1;
            HSSFSheet newsheet = null;

            //get custom col name
            List<string> customColNameList = columnNameDic.Values.ToList();
            List<string> originalColNameList = columnNameDic.Keys.ToList();

            //create  header cell style
            HSSFCellStyle headerCellStyle = (HSSFCellStyle)excelWorkbook.CreateCellStyle();
            headerCellStyle.Alignment = HorizontalAlignment.Center;
            headerCellStyle.VerticalAlignment = VerticalAlignment.Center;

            HSSFFont headerFont = (HSSFFont)excelWorkbook.CreateFont();
            headerFont.FontHeight = 20 * 20;
            headerFont.Boldweight = 700;
            headerCellStyle.SetFont(headerFont);

            //create new sheet
            newsheet = (HSSFSheet)excelWorkbook.CreateSheet("Sheet" + sheetCount);
            CreateHeader(newsheet, customColNameList, headerCellStyle);

            //create row
            foreach (T t in listSource)
            {
                rowCount++;
                if (rowCount == 10000)
                {
                    rowCount = 1;
                    sheetCount++;
                    newsheet = (HSSFSheet)excelWorkbook.CreateSheet("Sheet" + sheetCount);
                    CreateHeader(newsheet, customColNameList, headerCellStyle);
                }

                HSSFRow newRow = (HSSFRow)newsheet.CreateRow(rowCount);
                InsertCell(t, newRow, newsheet, excelWorkbook, originalColNameList);
            }
        }

        private static void InsertMergedHeaderRow<T>(
          List<T> listSource,
          HSSFWorkbook excelWorkbook,
          Dictionary<string, Dictionary<string, string>> columnNameDic)
        {
            int rowCount = 1;
            int sheetCount = 1;
            HSSFSheet newsheet = null;

            //get custom col name           
            List<string> originalColNameList = new List<string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> k in columnNameDic)
                originalColNameList.AddRange(k.Value.Keys);

            //create  header cell style
            HSSFCellStyle headerCellStyle = (HSSFCellStyle)excelWorkbook.CreateCellStyle();
            headerCellStyle.Alignment = HorizontalAlignment.Center;
            headerCellStyle.VerticalAlignment = VerticalAlignment.Center;

            HSSFFont headerFont = (HSSFFont)excelWorkbook.CreateFont();
            //headerFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
            headerFont.Boldweight = 700;
            headerCellStyle.SetFont(headerFont);

            //create new sheet
            newsheet = (HSSFSheet)excelWorkbook.CreateSheet("Sheet" + sheetCount);
            CreateMergedHeader(newsheet, columnNameDic, headerCellStyle);

            //create row
            foreach (T t in listSource)
            {
                rowCount++;
                if (rowCount == 10000)
                {
                    rowCount = 2;
                    sheetCount++;
                    newsheet = (HSSFSheet)excelWorkbook.CreateSheet("Sheet" + sheetCount);
                    CreateMergedHeader(newsheet, columnNameDic, headerCellStyle);
                }

                HSSFRow newRow = (HSSFRow)newsheet.CreateRow(rowCount);
                InsertCell(t, newRow, newsheet, excelWorkbook, originalColNameList);
            }
        }

        private static void CreateHeader(HSSFSheet excelSheet, List<string> customColNameList, HSSFCellStyle headerCellStyle)
        {
            int cellIndex = 0;
            foreach (string colName in customColNameList)
            {
                HSSFRow newRow = (HSSFRow)excelSheet.CreateRow(0);
                //create cell
                HSSFCell newCell = (HSSFCell)newRow.CreateCell(cellIndex);
                //set col width
                int colNameLength = colName.Length + 1;
                excelSheet.SetColumnWidth(cellIndex, Math.Max(colNameLength * 2, 6 * 2) * 256);
                //set cell style
                newCell.CellStyle = headerCellStyle;
                newCell.SetCellValue(colName);
                cellIndex++;
            }
        }

        private static void CreateMergedHeader(
            HSSFSheet excelSheet,
            Dictionary<string, Dictionary<string, string>> customColNameList,
            HSSFCellStyle headerCellStyle)
        {
            int cellIndex = 0;
            HSSFRow newRow0 = (HSSFRow)excelSheet.CreateRow(0);
            HSSFRow newRow1 = (HSSFRow)excelSheet.CreateRow(1);
            foreach (KeyValuePair<string, Dictionary<string, string>> kv in customColNameList)
            {
                string k = kv.Key;
                if (k == "LIST")
                {
                    foreach (KeyValuePair<string, string> col in customColNameList[k])
                    {
                        HSSFCell newCell0 = (HSSFCell)newRow0.CreateCell(cellIndex);
                        newCell0.CellStyle = headerCellStyle;
                        newCell0.SetCellValue(col.Value);
                        excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, cellIndex, 1, cellIndex));

                        //set col width
                        int colNameLength = col.Value.Length + 1;
                        excelSheet.SetColumnWidth(cellIndex, Math.Max(colNameLength * 2, 6 * 2) * 256);
                        cellIndex++;
                    }
                }
                else
                {
                    HSSFCell newCell0 = (HSSFCell)newRow0.CreateCell(cellIndex);
                    newCell0.CellStyle = headerCellStyle;
                    newCell0.SetCellValue(k);
                    excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, cellIndex, 0, cellIndex + customColNameList[k].Count - 1));
                    foreach (KeyValuePair<string, string> col in customColNameList[k])
                    {
                        HSSFCell newCell1 = (HSSFCell)newRow1.CreateCell(cellIndex);
                        newCell1.CellStyle = headerCellStyle;
                        newCell1.SetCellValue(col.Value);
                        //set col width
                        int colNameLength = col.Value.Length + 1;
                        excelSheet.SetColumnWidth(cellIndex, Math.Max(colNameLength * 2, 6 * 2) * 256);
                        cellIndex++;
                    }
                }
            }
        }

        private static void ContestReportCreateMergedHeader(
            HSSFSheet excelSheet,
            Dictionary<string, Dictionary<string, string>> customColNameList,
            Dictionary<string, HSSFCellStyle> headCellStyleList)
        {
            int cellIndex = 0;
            HSSFRow newRow0 = (HSSFRow)excelSheet.CreateRow(0);
            HSSFRow newRow1 = (HSSFRow)excelSheet.CreateRow(1);
            foreach (KeyValuePair<string, Dictionary<string, string>> kv in customColNameList)
            {
                string k = kv.Key;
                if (k == "LIST")
                {
                    foreach (KeyValuePair<string, string> col in customColNameList[k])
                    {
                        HSSFCell newCell0 = (HSSFCell)newRow0.CreateCell(cellIndex);
                        newCell0.CellStyle = headCellStyleList[k];
                        newCell0.SetCellValue(col.Value);
                        excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, cellIndex, cellIndex));
                        HSSFCell newCell1 = (HSSFCell)newRow1.CreateCell(cellIndex);
                        newCell1.CellStyle = headCellStyleList[k];
                        //set col width
                        int colNameLength = col.Value.Length + 1;
                        excelSheet.SetColumnWidth(cellIndex, Math.Max(colNameLength * 2, 6 * 2) * 256);
                        cellIndex++;
                    }
                }
                else
                {
                    HSSFCell newCell0 = (HSSFCell)newRow0.CreateCell(cellIndex);
                    excelSheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, cellIndex, cellIndex + customColNameList[k].Count - 1));
                    newCell0.CellStyle = headCellStyleList[kv.Key];
                    newCell0.SetCellValue(k);
                    foreach (KeyValuePair<string, string> col in customColNameList[k])
                    {
                        HSSFCell newCell1 = (HSSFCell)newRow1.CreateCell(cellIndex);
                        newCell1.CellStyle = headCellStyleList[kv.Key];
                        newCell1.SetCellValue(col.Value);
                        //set col width
                        int colNameLength = col.Value.Length + 1;
                        excelSheet.SetColumnWidth(cellIndex, Math.Max(colNameLength * 2, 6 * 2) * 256);
                        cellIndex++;
                    }
                }
            }
        }

        private static void InsertCell<T>(T source, HSSFRow currentExcelRow, HSSFSheet excelSheet, HSSFWorkbook excelWorkBook, List<string> originalColNameList)
        {
            for (int cellIndex = 0; cellIndex < originalColNameList.Count; cellIndex++)
            {
                string columnsName = originalColNameList[cellIndex];
                HSSFCell newCell = null;

                string cellValue = GetObjectPropertyValue<T>(source, columnsName);
                newCell = (HSSFCell)currentExcelRow.CreateCell(cellIndex);
                newCell.SetCellValue(cellValue);
            }
        }

        private static string GetObjectPropertyValue<T>(T t, string propertyName)
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        #region 调用NPIO组件导出
        public HSSFWorkbook m_xlApp = null;
        /// <summary>
        /// 获取当前Excel对象
        /// </summary>
        /// <returns></returns>
        public byte[] GetExcelByte
        {
            get
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    m_xlApp.Write(ms);
                    bytes = ms.ToArray();
                }
                return bytes;
            }

        }
        public ExcelBuilder()
        {
            m_xlApp = new HSSFWorkbook();
        }
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <param name="dic"></param>
        public void CreateExcel<T>(Dictionary<string, List<T>> dic, Dictionary<string, Dictionary<string, string>> columnMerge, Dictionary<string, ExcelCellColor> headerStyles)
        {

            int Count = 1;
            //遍历Dictionary
            foreach (KeyValuePair<string, List<T>> sheetItem in dic)
            {
                string sheetKey = sheetItem.Key.ToString();//键
                HSSFSheet worksheet = (HSSFSheet)m_xlApp.CreateSheet(sheetKey);//创建Sheet
                List<T> sheetValue = (List<T>)sheetItem.Value;//值

                FillSheet(worksheet, sheetValue, columnMerge, headerStyles);

                Count++;
            }
        }

        /// <summary>
        /// 根据DataTable填充Sheet
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="dt"></param>
        private void FillSheet<T>(HSSFSheet worksheet, List<T> objecList, Dictionary<string, Dictionary<string, string>> columnMerge, Dictionary<string, ExcelCellColor> headerStyles)
        {

            #region 设置头样式

            Dictionary<string, HSSFCellStyle> headCellStyleList = new Dictionary<string, HSSFCellStyle>();

            foreach (string key in columnMerge.Keys)
            {
                HSSFCellStyle tmpCellStyle = this.GetContestHeadCellStyle;
                tmpCellStyle.FillForegroundColor = (short)headerStyles[key];
                tmpCellStyle.FillPattern = FillPattern.SolidForeground;

                headCellStyleList.Add(key, tmpCellStyle);
            }

            #endregion

            #region 设置内容样式
            HSSFCellStyle contentCellStyle = (HSSFCellStyle)m_xlApp.CreateCellStyle();
            contentCellStyle.Alignment = HorizontalAlignment.Center;
            contentCellStyle.VerticalAlignment = VerticalAlignment.Center;
            contentCellStyle.BorderTop = BorderStyle.Thin;
            contentCellStyle.BorderBottom = BorderStyle.Thin;
            contentCellStyle.BorderLeft = BorderStyle.Thin;
            contentCellStyle.BorderRight = BorderStyle.Thin;

            HSSFFont contentFont = (HSSFFont)m_xlApp.CreateFont();
            contentFont.FontHeightInPoints = 10;
            contentFont.Boldweight = 700;
            contentCellStyle.SetFont(contentFont);
            #endregion

            ContestReportCreateMergedHeader(worksheet, columnMerge, headCellStyleList);

            int dataRowCount = 2;

            foreach (T t in objecList)
            {
                HSSFRow row = (HSSFRow)worksheet.CreateRow(dataRowCount);
                int columnCount = 0;
                foreach (KeyValuePair<string, Dictionary<string, string>> columnItem in columnMerge)
                {
                    foreach (KeyValuePair<string, string> item in columnItem.Value)
                    {
                        string key = item.Key.ToString();//物理列
                        string value = item.Value.ToString();//显示列
                        string cellValue = GetObjectPropertyValue(t, key);
                        HSSFCell contentCell = (HSSFCell)row.CreateCell(columnCount);
                        contentCell.CellStyle = contentCellStyle;
                        contentCell.SetCellValue(cellValue);

                        columnCount++;
                    }
                }
                dataRowCount++;
            }
        }
        /// <summary>
        /// 头公共属性设置
        /// </summary>
        private HSSFCellStyle GetContestHeadCellStyle
        {
            get
            {
                HSSFCellStyle handCellStyle = (HSSFCellStyle)m_xlApp.CreateCellStyle();
                handCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //HSSFCellStyle.ALIGN_CENTER;
                handCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                handCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                handCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                handCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                handCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                HSSFFont handFont = (HSSFFont)m_xlApp.CreateFont();
                handFont.FontHeightInPoints = 10;
                handFont.Boldweight = 700;
                //handFont.Boldweight = HSSFFont.BOLDWEIGHT_BOLD;
                handFont.FontHeightInPoints = 10;
                handCellStyle.SetFont(handFont);
                return handCellStyle;
            }
        }
        #endregion
    }
}



