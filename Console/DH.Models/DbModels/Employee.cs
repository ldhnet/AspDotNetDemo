
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Framework.Utility;
using Framework.Utility.Attributes;

namespace DH.Models.DbModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCard { get; set; }
        public string EmployeeName { get; set; }   
        public string EmployeeSerialNumber { get; set; } 
        public int? Department { get; set; }
        public string Phone { get; set; }
    }
}
