using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    [Serializable]
    public class UserCacheModel
    {
        public UserCacheModel()
        {
            ViewOrgIds = new Tuple<List<int>, long>(new List<int>(), 0);
        } 
        /// <summary>
        /// 人事ID
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// 人事编号 （目前同 EmployeeId）
        /// </summary>
        public string EmployeeSerialNumber { get; set; }
        public string EmployeeName { get; set; }

        public string EnglishName { get; set; }


        public int OrgId { get; set; }

        public List<int> ApprovalOrgIds { get; set; }

        public Tuple<List<int>, long> ViewOrgIds { get; set; }
          
        //人员角色合集
        public List<int> ViewRoleIds { get; set; }
          
        public string PortraitFileName { get; set; } 

        public bool IsDimission { get; set; } 

        /// <summary>
        /// 必须强制密码更新
        /// </summary>
        public bool MustChangePassword { get; set; }

        /// <summary>
        /// 汇报对象
        /// </summary>
        public string ReportTo { get; set; } = string.Empty;

        public DateTime LoginTime { get; set; }
    }
}
