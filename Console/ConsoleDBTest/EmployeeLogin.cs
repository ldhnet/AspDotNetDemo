using System;

namespace ConsoleDBTest
{
    public class EmployeeLogin
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LoginTime { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
