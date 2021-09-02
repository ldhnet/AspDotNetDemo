using DHLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Order]
        [ImportedColumn(Caption ="姓名")]
        public string EmployeeName { get; set; }
        [Order]
        [ImportedColumn(Caption = "年龄")]
        public string Age { get; set; }
        public string EnglishName { get; set; }

        public string EmployeeSerialNumber { get; set; }

        public int Department { get; set; }
        [Order]
        [ImportedColumn(Caption = "手机号")]
        public string Phone { get; set; }

        [Encrypted]
        public string BankCard { get; set; }
        public string BankCardDisplay { get; set; }

        [Encrypted]
        public string Monery { get; set; }
        [MinLength(100)]
        public string MoneryDisplay { get; set; } 
    }
}
