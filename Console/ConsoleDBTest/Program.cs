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


            Employee zhangsan = new Employee { Name = "张三", BankCard = "12345", BankCardDisplay = "12345", Monery = "11.1", MoneryDisplay = "11.1" };
            Employee lisi = new Employee { Name = "李四", BankCard = "67890", BankCardDisplay = "67890", Monery = "12.2", MoneryDisplay = "11.1" };
            context.Employees.AddRange(zhangsan, lisi);
            context.SaveChanges();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id} {user.Name} {user.BankCard}");
            }
             

            //Console.WriteLine("Hello World!");
        }
       
    }
}
