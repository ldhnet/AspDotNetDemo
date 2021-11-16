namespace ConsoleDBTest
{
    public class EmployeeExtend
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Phone { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
