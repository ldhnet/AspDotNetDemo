using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Constant
{
    public class WebConstant
    {
        public struct ExportReportFileName
        {
            public const string NewLeaveEmp = "新录入与离职报表{0}.xls";
            public const string LeaveTime = "休假报表报表{0}.xls";
            public const string YearLeave = "年假报表{0}.xls";
            public const string MonthBirthday = "月度员工生日报表{0}.xls";
            public const string EmployeeChanges = "人员变更报表{0}.xls";
            public const string EmployeeOvertime = "加班报表{0}.xls";
            public const string AgentAttendanceDetail = "{0}-{1}明细报表.xls";
            public const string AgentAttendanceSummary = "考勤汇总报表{0}.xls";
            public const string BankSalary = "银行报盘报表{0}.xls";
            public const string OrganizationSetReport = "部门设置报表{0}.xls";
        }

    }
}
