using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDBTest
{
    public class EmployeeLogin
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }    
        public DateTime LoginTime { get; set; }
         
        public virtual Employee Employee { get; set; }
    }
}
