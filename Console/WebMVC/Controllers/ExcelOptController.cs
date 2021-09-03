using DHLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.Extension;
using WebMVC.Helper;
using WebMVC.Model;

namespace WebMVC.Controllers
{
    public class ExcelOptController : BaseController
    { 
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

        private readonly string _saveFilePath = "C:\\LDHReport";
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
                new Employee{EmployeeName="张9",EnglishName="zhangsan",EmployeeSerialNumber="00005",Phone="15555555555" }
            };
            Dictionary<string, List<Employee>> contestdic = new Dictionary<string, List<Employee>>();
            contestdic.Add("人员信息报表", ExportList);

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
    }
}
