using DH.Models.Entities;
using Framework.Utility.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Models.Dtos
{ 
    public class EmployeeLoginDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
