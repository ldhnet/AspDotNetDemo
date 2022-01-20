using Framework.Utility.Helper;
using Framework.Utility.IO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    public class ExcelPackageHelper
    {
        public Stream GererateReport(List<ExcelClassInfo> list)
        {
            if (list.Count == 0) throw new Exception("数据为空");

            var stream = new MemoryStream();
            var newFileName = $"报表_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls";
            var newFile = GenerateNewFileInfo(@"C:/demo/", "报表模板.xls", newFileName);

            var type = typeof(ExcelClassInfo);
            var properties = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(ExportColumnAttribute), true).Length > 0).ToList();

            //起始行 = 5
            int startRowNum = 5, rowNum = 5;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(newFile))
            { 
                var worksheet = package.Workbook.Worksheets.Add("test"); 
                worksheet.Cells["A2"].Value = $"操作日期:{DateTime.Now.ToString()}";
                var columnNum = 0;
                worksheet.InsertRow(startRowNum, list.Count);

                foreach (var item in list)
                {
                    columnNum = 0;
                    foreach (var property in properties)
                    {
                        var column = AlphabetHelper.GetExcelAlphabeticTag(columnNum);
                        var cellAddress = $"{column}{rowNum}";

                        var val = property.GetValue(item);

                        worksheet.Cells[cellAddress].Value = val;

                        if (property.PropertyType.FullName == "System.DateTime" || property.PropertyType.FullName.Contains("System.Nullable`1[[System.DateTime"))
                        {
                            worksheet.Cells[cellAddress].Style.Numberformat.Format = "yyyy-MM-dd";
                        }

                        if (property.Name == "MoneyCount")
                        {
                            worksheet.Cells[cellAddress].Style.Font.Color.SetColor(Color.Red);
                        }

                        columnNum++;
                    }
                    rowNum++;
                }

                package.Workbook.Calculate();
                package.SaveAs(stream);
            }
            stream.Position = 0;
            newFile.Delete();
            return stream;
        }

        private FileInfo GenerateNewFileInfo(string pathFile, string templateFileName, string newFileName)
        {
            var template = FileHelper.GenerateFileInfo(pathFile, templateFileName, false); 
            var destFileName = Path.Combine(pathFile, newFileName);
            return template.CopyTo(destFileName, true);
        }
    }
}
