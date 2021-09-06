using DHLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.Report
{
    #region 福利假期报表
    public class YearLeaveModel
    {
        [Order]
        [ColumnExportFormat("序号", ExcelCellFormat.IntegerFormat)]
        public int No { get; set; }

        [Order]
        [ColumnExportFormat("姓名", ExcelCellFormat.StringFormat)]
        public string EmployeeName { get; set; }

        [Order]
        [ColumnExportFormat("员工编号", ExcelCellFormat.LongNumStringFormat)]
        public string EmployeeSerialNumber { get; set; } 
         
    }
    #endregion
}
