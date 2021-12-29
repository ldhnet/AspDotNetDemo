using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleDBTest.Entites
{
    public class EmployeeExtend
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public virtual Employee Employee { get; set; }
    }
}
