using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDBTest.Entites
{
    public class EmployeeDetail
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EnglishName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
