using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string BankCard { get; set; }
        public string BankCardDisplay { get; set; } 
        public string Monery { get; set; }

        public string MoneryDisplay { get; set; } 
        public virtual EmployeeExtend EmployeeExtend { get; set; }

        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }

    }
}
