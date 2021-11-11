using Framework.Utility;
using Framework.Utility.Attributes;
using Framework.Utility.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXml4Net.OPC.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Helper;
using WebMVC.Model;
using WebMVC.Models;
using WebMVC.Models.Report;

namespace WebMVC.Controllers
{
    public class ExcelOptController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// execl导入
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult ExportExecl()
        {
            var file = Request.Form.Files;
            var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');

            if (string.Empty.Equals(filename) || ".xlsx" != Path.GetExtension(filename))
            {
                throw new ArgumentException("当前文件格式不正确,请确保正确的Excel文件格式!");
            }
            var severPath = CoreHttpContext.MapPath("prodplanfile");//获取当前虚拟文件路径

            var savePath = Path.Combine(severPath, filename); //拼接保存文件路径

            try
            {
                if (!Directory.Exists(severPath))
                    Directory.CreateDirectory(severPath);

                //FileStream filew = new FileStream(savePath, FileMode.OpenOrCreate);
                //file[0].CopyTo(filew);
                //filew.Flush();
                //filew.Dispose();

                using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }


                var dt = DateTableHelper.ReadExcel(savePath);


                var aaa = dt.ConvertDataTableToList2<Employee>();


                //var stus = ReadExcelToEntityList<prodplan>(savePath);//插入数据
                //if (stus != null)
                //{
                //    string time = stus[0].plan_date;
                //    ViewBag.dateTime = Convert.ToDateTime(time).ToString("yyyy");
                //}

                return View("Index");
            }
            finally
            {
                //System.IO.File.Delete(savePath);//每次上传完毕删除文件
            }

        }

        private readonly string _saveFilePath = GlobalContext.HostingEnvironment.WebRootPath + "Report";
        [HttpPost] 
        public ActionResult ExportExeclReport()
        {
            Dictionary<string, Dictionary<string, string>> columnMerge = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> CommColumnNameDic = new Dictionary<string, string>();

            Dictionary<string, ExcelCellColor> contestColumnMergeStyles = new Dictionary<string, ExcelCellColor>();
            Dictionary<string, ExcelCellColor> paymentColumnMergeStyles = new Dictionary<string, ExcelCellColor>();



            Dictionary<string, Dictionary<string, string>> paymentMerge = new Dictionary<string, Dictionary<string, string>>();

            CommColumnNameDic.Add("EmployeeName", "姓名");
            CommColumnNameDic.Add("EnglishName", "英文名");
            CommColumnNameDic.Add("EmployeeSerialNumber", "编号");
            CommColumnNameDic.Add("Phone", "手机号");
            columnMerge.Add("LIST", CommColumnNameDic);
            contestColumnMergeStyles.Add("LIST", ExcelCellColor.SKY_BLUE);


            paymentMerge.Add("LIST3", CommColumnNameDic);
            paymentColumnMergeStyles.Add("LIST3", ExcelCellColor.ORANGE);

            List<Employee> ExportList = new List<Employee>() {
                new Employee{EmployeeName="张三",EnglishName="zhangsan",EmployeeSerialNumber="00001",Phone="15000000000" },
                new Employee{EmployeeName="张4",EnglishName="zhangsan",EmployeeSerialNumber="00006",Phone="15111111111" },
                new Employee{EmployeeName="张5",EnglishName="zhangsan",EmployeeSerialNumber="00002",Phone="15222222222" },
                new Employee{EmployeeName="张6",EnglishName="zhangsan",EmployeeSerialNumber="00003",Phone="15333333333" },
                new Employee{EmployeeName="张7",EnglishName="zhangsan",EmployeeSerialNumber="00004",Phone="15444444444" },
                new Employee{EmployeeName="张9",EnglishName="zhangsan",EmployeeSerialNumber="00005",Phone="15555555555" },
                new Employee{EmployeeName="张10",EnglishName="zhangsan",EmployeeSerialNumber="00005",Phone="1666666666666" }
            };
            Dictionary<string, List<Employee>> contestdic = new Dictionary<string, List<Employee>>();
            contestdic.Add("人员信息报表1", ExportList);

            ExcelBuilder excelBuilder = new ExcelBuilder();
            excelBuilder.CreateExcel(contestdic, columnMerge, contestColumnMergeStyles);


            Dictionary<string, List<Employee>> paymentdic = new Dictionary<string, List<Employee>>();
            paymentdic.Add("测试多sheet报表", ExportList);
            excelBuilder.CreateExcel(paymentdic, paymentMerge, paymentColumnMergeStyles);

            byte[] contestReport = excelBuilder.GetExcelByte;

            var folderPath = Path.Combine(_saveFilePath, $"{DateTime.Now.ToString("yyyyMMdd")}人员信息报表");
            var fileName = $"{DateTime.Now.ToString("yyyy-MM-dd")}人员信息报表.xls";
            string filePath = Path.Combine(folderPath, fileName);

            ExportToExcel(contestReport, folderPath, filePath);



            return View("Index");

        }
        private void ExportToExcel(byte[] bytes, string folderPath, string filePath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            MemoryStream m = new MemoryStream(bytes);
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);
            m.WriteTo(file);
            m.Close();
            file.Dispose();
        }

        [HttpPost]
        public ActionResult ExportExeclReport2()
        {
            List<YearLeaveModel> ExportList = new List<YearLeaveModel>() {
                 new YearLeaveModel{EmployeeName="张3",EmployeeSerialNumber="00001",No=1 },
                 new YearLeaveModel{EmployeeName="张4",EmployeeSerialNumber="00002",No=2 },
                 new YearLeaveModel{EmployeeName="张5",EmployeeSerialNumber="00003",No=3 },
                 new YearLeaveModel{EmployeeName="张6",EmployeeSerialNumber="00004",No=4 },
                 new YearLeaveModel{EmployeeName="张7",EmployeeSerialNumber="00005",No=5 },
                 new YearLeaveModel{EmployeeName="张8",EmployeeSerialNumber="00006",No=6 },
            };

           var mm=  GererateWelfareLeaveReport(ExportList);

            //var folderPath = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, "DownLoad");
            //if (!Directory.Exists(folderPath))
            //    Directory.CreateDirectory(folderPath);



            //FileStream file = new FileStream(folderPath, FileMode.OpenOrCreate);
            //mm.WriteTo(file);
            //mm.Close();
            //file.Dispose();

            return View("Index");
        }
        public MemoryStream GererateWelfareLeaveReport(List<YearLeaveModel> list)
        {
            if (list.Count == 0) throw new Exception("数据为空");

            var stream = new MemoryStream();
            var newFileName = $"人员信息报表_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            var newFile = GenerateNewFileInfo(Path.Combine(GlobalContext.HostingEnvironment.WebRootPath, "Temp"), "人员信息报表.xlsx", newFileName);

            var type = typeof(YearLeaveModel);
            var properties = type.GetProperties().Where(p => p.GetCustomAttributes(typeof(ColumnExportFormatAttribute), true).Length > 0).ToList();

            //起始行 = 5
            int startRowNum = 5, rowNum = 5;
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets[1];
                worksheet.Cells["A2"].Value = $"操作日期:{DateTime.Now.ParseToString()}";

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

                        if (item.No >5 && property.Name == "AnualIsSet")
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

        private FileInfo GenerateNewFileInfo(string path, string templateFileName, string newFileName)
        {
            var folderPath = Path.Combine(GlobalContext.HostingEnvironment.WebRootPath, "DownLoad");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var template =  FileToolHelper.GenerateFileInfo(path, templateFileName, false); 
            var destFileName = folderPath +  $"{Path.AltDirectorySeparatorChar}{newFileName}";

            return template.CopyTo(destFileName, true);
        }

    }
}
