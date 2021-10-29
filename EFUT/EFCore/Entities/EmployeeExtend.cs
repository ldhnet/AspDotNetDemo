using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    public class EmployeeExtend
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }    
        public string Phone { get; set; }
         
        public virtual Employee Employee { get; set; }
    }
}
