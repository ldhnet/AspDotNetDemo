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
        public static void DoSomething2(Content test)
        {
          
            Console.WriteLine(test.ToString());
        }

        static void Main(string[] args)
        {
           
            var contents = new List<Content>();
            for (int i = 0; i < 10; i++)
            {
                contents.Add(new Content { Id = i, Title = $"第{i}条数据标题", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now.AddDays(-i) });
            }
 
            Parallel.ForEach(contents, (i) => {
                try
                {
                    DoSomething2(i);
                }
                catch (Exception ex)
                {

                    throw;
                }
         
            });


            ParallelLoopResult result = Parallel.For(0, 10, i =>
            {
                Console.WriteLine("迭代次数：{0},任务ID:{1},线程ID:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            });

            Console.WriteLine("是否完成:{0}", result.IsCompleted);
            Console.WriteLine("最低迭代:{0}", result.LowestBreakIteration);



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
