using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TitleAttribute: Attribute
    {
        public string Title { get; set; }
        //public HSSFellStyle CellType { get; set; }
    }
}
