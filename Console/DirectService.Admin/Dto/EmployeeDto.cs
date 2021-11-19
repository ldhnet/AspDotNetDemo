using DH.Models.DbModels;
using Framework.Core.Data;
using Framework.Utility.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectService.Admin.Dto
{
    [MapperInit(sourceType: typeof(Employee), targetType: typeof(EmployeeDto))]
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

        public virtual EmployeeDetail EmployeeDetail { get; set; }

        public virtual ICollection<EmployeeLogin> EmployeeLogins { get; set; } = new List<EmployeeLogin>();
    }
}
