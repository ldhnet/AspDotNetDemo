﻿using DH.Models.Entities;
using Framework.Core.Data;
using Framework.Utility.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.Dtos
{ 
    public class EmployeeDto: IAddDto, IEditDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankCard { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSerialNumber { get; set; }
        public int? Department { get; set; }
        public string Phone { get; set; }

        public string WebToken { get; set; }
        public string ApiToken { get; set; }
        public DateTime? ExpirationDateUtc { get; set; }

        public EmployeeDetailDto EmployeeDetail { get; set; }

        public List<EmployeeLoginDto> EmployeeLogins { get; set; } = new List<EmployeeLoginDto>();
    }
}
