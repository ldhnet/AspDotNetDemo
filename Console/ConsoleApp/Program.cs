using DHLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMVC.Models;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            Parallel.ForEach(nums, (item) =>
            {
                Console.WriteLine("针对集合元素{0}的一些工作代码……ThreadId={1}", item, Thread.CurrentThread.ManagedThreadId);
            });


            Console.ReadKey();
            var contents = new List<Content>();
            for (int i = 0; i < 10; i++)
            {
                contents.Add(new Content { Id = i, Title = $"第{i}条数据标题", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now.AddDays(-i) });
            }
            var all = new[] { 0, 1, 2, 3, 4, 5, 6 };

            Parallel.For(0, contents.Count(), (i) => {
                Console.Write(contents[i].ToString());
            });
             

            //


            Parallel.For(0, 1000, (i) => {
                Console.Write($"{i} ");
            });
            Console.WriteLine();


        

            //Console.WriteLine("Hello World!");

            //var options = new DbContextOptionsBuilder<CodeDbContext>().UseInMemoryDatabase(databaseName: "MyInMemoryDatabase").Options;
            //CodeDbContext context = new CodeDbContext(options);

            ////Employee zhangsan = new Employee { Name = "张三", BankCard = "12345" };
            ////Employee lisi = new Employee { Name = "李四", BankCard = "67890" };
            ////context.Employee.AddRange(zhangsan, lisi);
            ////context.SaveChanges();

            //var users = context.Users.ToList();

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
