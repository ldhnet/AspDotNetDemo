using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks; 

namespace PressureTest
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string EmployeeName { get; set; } 
        public string Age { get; set; }
        public string EnglishName { get; set; }

        public string EmployeeSerialNumber { get; set; }

        public int? Department { get; set; } 
        public string Phone { get; set; }
         
        public string BankCard { get; set; } 
        public string BankCardDisplay { get; set; }
         
        public string Monery { get; set; } 
        public string MoneryDisplay { get; set; }
        public override string ToString()
        {
            return $"{Name} -- {BankCardDisplay}";
        }
    }
}
