using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Encrypted]
        public string BankCard { get; set; }
        public string BankCardDisplay { get; set; }

        [Encrypted]
        public string Monery { get; set; }

        public string MoneryDisplay { get; set; }
        public virtual EmployeeExtend EmployeeExtend { get; set; }

        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; }

    }
}
