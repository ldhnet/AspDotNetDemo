using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.Entities
{
    public class EmployeeDetail
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EnglishName { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual Employee Employee { get; set; }
    }
}
