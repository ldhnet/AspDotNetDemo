using Framework.Utility.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.ExportModel
{
    public class ClassInfo  
    {
        [Title(Title="用户ID")]
        public int UserId { get; set; }
        [Title(Title = "用户名称")]
        public string UserName { get; set; }
        [Title(Title = "用户年龄")]
        public int Age { get; set; }
        [Title(Title = "用户类型")]
        public int UserType { get; set; }
        [Title(Title = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [Title(Title = "用户描述")]
        public string Description { get; set; }
    }
}
