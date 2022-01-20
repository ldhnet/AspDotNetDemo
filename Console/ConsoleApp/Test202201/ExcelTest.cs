using DH.Models.ExportModel;
using Framework.Utility.Excel;
using Framework.Utility.Helper;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test202201
{
    public  class ExcelTest
    {
        public static void importExcelTest()
        {
            var dataTableList = new List<DataTable>();

            using (FileStream file = new FileStream("C:\\demo\\StudentInfo.xls", FileMode.Open))
            {
                dataTableList = ExcelHelper.ExcelStreamToDataTable(file);
            }

            var dataTableJson = JsonHelper.ToJson(dataTableList);
        }
        public static void exportExcelTest()
        {

            List<object> objList = new List<object>();

            for (int i = 0; i < 10; i++)
            {
                objList.Add(new ClassInfo()
                {
                    UserId = i + 1,
                    UserName = $"姓名{i}",
                    Age = i + 20,
                    UserType = i + 1,
                    CreateTime = DateTime.Now,
                    Description = $"objList描述{i}"
                });
            }


            List<object> objList2 = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                objList2.Add(new ClassInfo()
                {
                    UserId = i + 1,
                    UserName = $"姓名{i}",
                    Age = i + 20,
                    UserType = i + 1,
                    CreateTime = DateTime.Now,
                    Description = $"objList2描述{i}"
                });
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
             

            IWorkbook workbook = ExcelHelper.DataToHSSFWorkbook(dataResources);

            using (FileStream file = new FileStream("C:\\demo\\StudentInfo.xls", FileMode.Create))
            {
                workbook.Write(file);
            }

        }
    }
}
