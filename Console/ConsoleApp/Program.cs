using DHLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal; 
using System;
using System.Linq;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var options = new DbContextOptionsBuilder<CodeDbContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;
            CodeDbContext context = new CodeDbContext(options);

            //Employee zhangsan = new Employee { Name = "张三", BankCard = "12345" };
            //Employee lisi = new Employee { Name = "李四", BankCard = "67890" };
            //context.Employee.AddRange(zhangsan, lisi);
            //context.SaveChanges();

            var users = context.Users.ToList();

            //var aa = SwitchHelper.FromRainbow(Rainbow.brack);

            //var address = new Address() { State = "MN" };

            //var bb = SwitchHelper.ComputeSalesTax(address, 100);

            //var cc = SwitchHelper.RockPaperScissors("scissors", "paper");

            //DouDiZhuHelper douDiZhuHelper = new DouDiZhuHelper();
            //douDiZhuHelper.DouDiZhu();

        }

        public void DoSomething(int _)
        {
             _ = SwitchHelper.FromRainbow(Rainbow.brack);
        }
    }
}
