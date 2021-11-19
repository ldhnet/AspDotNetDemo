using DH.Models.Dtos;
using Framework.Utility.Mapping;

namespace DH.Models.Entities
{
    [MapperInit(sourceType: typeof(EmployeeDto), targetType: typeof(Employee))]
    public class Employee
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
