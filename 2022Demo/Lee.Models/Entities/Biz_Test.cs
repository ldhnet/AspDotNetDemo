namespace Lee.Models.Entities
{ 
    public class Biz_Test
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string Phone { get; set; }
        public string? BankCard { get; set; }
        public EmployeeStatus TestStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime CreateTime { get; set; }
         
        public DateTime? UpdateTime { get; set; } 
         
    }
 
}
