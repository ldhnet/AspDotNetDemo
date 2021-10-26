using ConsoleDBTest.Context;
using Microsoft.Extensions.Options;
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

 
            DefaultDbContext context = new DefaultDbContext();

           
            var users = context.Employees.ToList();


            Employee zhangsan = new Employee { Name = Guid.NewGuid().ToString(), BankCard = "123456789101112131415", BankCardDisplay = "123456789101112131415", Monery = "11.1", MoneryDisplay = "11.1" };
            Employee lisi = new Employee { Name = Guid.NewGuid().ToString(), BankCard = "987654312222222233344", BankCardDisplay = "987654312222222233344", Monery = "12.2", MoneryDisplay = "11.1" };
            context.Employees.AddRange(zhangsan, lisi);
            context.SaveChanges();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id} {user.Name} {user.BankCardDisplay} {user.BankCard}");
            }
             

            //Console.WriteLine("Hello World!");
        }
       
    }
}
