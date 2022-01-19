using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Excel
{
    public class ExcelDataResource
    {
        public int TitleIndex { get; set; }
        public string SheetName { get; set; }
        public List<object> SheetDataResource { get; set; }
    }
}
