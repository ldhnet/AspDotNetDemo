using ConsoleDBTest.Entites;
using ConsoleDBTest.Helper;
using System;
using System.Linq;

namespace ConsoleDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //AdoInsertTest adoInsertTest= new AdoInsertTest();

            //adoInsertTest.GetData();

            //int retsult= adoInsertTest.Add();

            SqlHelper sqlHelper = new SqlHelper();
            EmployeeExtend employee1 = sqlHelper.Find<EmployeeExtend>(1);
             
            EmployeeExtend employee2 = sqlHelper.Find<EmployeeExtend>(2);



            DefaultDbContext context = new DefaultDbContext();


            var users = context.Employees.ToList();


            Employee zhangsan = new Employee
            {
                Name = Guid.NewGuid().ToString(),
                BankCard = "123456789101112131415",
                BankCardDisplay = "123456789101112131415",
                Monery = "11.1",
                MoneryDisplay = "11.1"
            };
            zhangsan.EmployeeExtend = new EmployeeExtend
            {
                Phone = "15225074031",
            };
            //Employee lisi = new Employee { 
            //    Name = Guid.NewGuid().ToString(), 
            //    BankCard = "987654312222222233344", 
            //    BankCardDisplay = "987654312222222233344",
            //    Monery = "12.2", 
            //    MoneryDisplay = "11.1"
            //};

            context.Employees.AddRange(zhangsan);
            context.SaveChanges();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id} {user.Name} {user.BankCardDisplay} {user.BankCard} {user.EmployeeExtend.Phone}");
            }


            //Console.WriteLine("Hello World!");
        }

    }
}
