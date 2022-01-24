
using ConsoleApp.Gof.builder;
using DH.Models.ExportModel;
using Framework.Utility.Excel;
using Framework.Utility.Helper;
using Framework.Utility.IO;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Test202201
{
    public  class ExcelTest
    {
        public const string Template = "~/Template";
        public static void importExcelTest()
        {
            IWorkbook workbook =  UpdateImportTemplate.UpdateBatchImportTemplate("");

            using (FileStream file = new FileStream($"C:\\demo\\StudentInfo-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls", FileMode.Create))
            {
                workbook.Write(file);
            }
             
            var dataTableList = new List<DataTable>();

            using (FileStream file = new FileStream("C:\\demo\\StudentInfo.xls", FileMode.Open))
            {
                dataTableList = ExcelHelper_Bk.ExcelStreamToDataTable(file);
            }

            var dataTableJson = JsonHelper.ToJson(dataTableList);
        }
        public static void exportExcelTest()
        { 
            List<object> objList_1 = new List<object>(); 

            List<object> objList = new List<object>();

            for (int i = 0; i < 10; i++)
            {
                var ttModel = new ClassInfo()
                {
                    UserId = i + 1,
                    UserName = $"姓名{i}",
                    Age = i + 20,
                    UserType = i + 1,
                    CreateTime = DateTime.Now,

                    MoneyCount = i + 10.217890m,

                    PointCount = i + 180.212567000f,

                    Description = $"objList描述{i}"
                };
                var ttModel222 = new ExcelClassInfo()
                {
                    UserId = i + 1,
                    UserName = $"姓名{i}",
                    Age = i + 20,
                    UserType = i + 1,
                    CreateTime = DateTime.Now,

                    MoneyCount = i + 10.217890m,

                    PointCount = i + 180.212567000f,

                    Description = $"objList描述{i}"
                };
                objList.Add(ttModel);
                 
                objList_1.Add(ttModel222);
            }


            List<object> objList2 = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                var ttmodel2 = new ClassInfo()
                {
                    UserId = i + 1,
                    UserName = $"姓名{i}",
                    Age = i + 20,
                    UserType = i + 1,
                    CreateTime = DateTime.Now,

                    MoneyCount = i + 20.21256m,

                    PointCount = i + 280.6621256f,

                    Description = $"objList2描述{i}"
                };
                objList2.Add(ttmodel2); 
            }


            List<ExcelDataResource> dataResources = new List<ExcelDataResource>() {
                new ExcelDataResource(){
                    SheetName ="页签一",
                    TitleIndex = 3,
                    SheetDataResource = objList
                },
                 new ExcelDataResource(){
                    SheetName ="页签二",
                    TitleIndex = 1,
                    SheetDataResource = objList2
                }
            }; 

            //IWorkbook workbook = ExcelComplexHelper.DataToHSSFWorkbook(dataResources);

            //using (FileStream file = new FileStream($"C:\\demo\\StudentInfo-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls", FileMode.Create))
            //{
            //    workbook.Write(file);
            //}
             
            var fileName = $"报表_{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls"; 
            var templateFile = @"C:\\demo\\报表模板.xls";
            IWorkbook aaaworkbook = ExcelHelper.DataToHSSFWorkbook(objList_1, templateFile);
             
            using (FileStream file = new FileStream($"C:\\demo\\{fileName}.xls", FileMode.Create))
            {
                aaaworkbook.Write(file);
            }

        }




   

    }
}
