using DHLibrary; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMVC.Models;

namespace ConsoleApp
{

    class Program
    {
        public static Func<int, int, int> fc = (int x, int y) => x * y;
        public static void DoSomething2(Content test)
        {
            if (test.Id < 100)
            {
                Console.WriteLine(test.ToString() + $"任务ID:{Task.CurrentId},线程ID:{Thread.CurrentThread.ManagedThreadId}");
            }

   

            //var list = new List<int>();
            //list.EnsureCapacity(5);
            //for (var i = 0; i < 5; i++)
            //{
            //    list.Add(i);
            //    Console.WriteLine(list[i]);
            //} 
            //Console.WriteLine(list[5]);
        }
        public void GetSendMailList()
        {
            int k = 0;
            while (true)
            {
                Console.WriteLine(k);
                if (k == 10)
                {
                    //如果i等于10，就跳出循环
                    break;
                }
                k++;
            }
        }

        static void Main(string[] args)
        { 
            Dictionary<int, List<int>> diDic = new Dictionary<int, List<int>>();


            for (int i = 1; i < 5; i++)
            {
                diDic.Add(i,new List<int>() { i+1 });
                if (i==2)
                {
                    diDic[i].Add(i + 2);
                }
            }

            foreach (int denpendencyType in diDic.Keys)
            {
                foreach (int injectType in diDic[denpendencyType])
                {
                    var aa = injectType;
                }

            }

            //var idList = new List<long>();
            //IdWorker idworker = new IdWorker(1);
            //for (int i = 0; i < 1000; i++)
            //{
            //    idList.Add(idworker.nextId());
            //}

            //foreach (var item in idList.OrderBy(c => c))
            //{
            //    Console.WriteLine(item);
            //}

            //var _fcR = fc(5,2);

            //int[] dataArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //ShuffleCopyHelper shuffle=new ShuffleCopyHelper();

            //var resultArr =new List<int>();

            //var r = new Random();


            //var _array = new[] {11,10,50,100, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

           
            //Array.Sort(_array);

            //Array.Reverse(_array);

            //var aaaaAry = _array;


            //for (var times = 0; times < 1000; ++times)
            //{
            //    var winner = shuffle.ShuffleCopy(dataArr, r).First();
            //    resultArr.Add(winner);
            //}
             
            //var resultGr = resultArr.GroupBy(g => g).Select(x=> new { value =x.Key ,count=x.Count() }).OrderBy(o=>o.value);

            //var resultGrs = resultGr.Select(x => new { resKay = x.value + "-->" + x.count }).Select(c=>c.resKay).ToArray();

            //var resultStr = string.Join(",", resultGrs);

            //Console.WriteLine(resultStr);


            //var resultList = shuffle.Measure(10, 50).Select((v, i) => new { X = i, Y = v });

            //var str = string.Join(",", resultList);

            //Console.WriteLine(str);

             
            //var aaa = Math.Ceiling(49 / 0.75 / 4);


            //Task task1 = new Task(() =>  Console.WriteLine($"hello, task 1的线程ID{Thread.CurrentThread.ManagedThreadId}")); 
            //task1.Start();

            //Task task2 = Task.Run(() => { Console.WriteLine($"hello, task 2的线程ID{Thread.CurrentThread.ManagedThreadId}"); });

            //Task task3 = Task.Factory.StartNew(() => { Console.WriteLine($"hello,task 3的线程ID为{Thread.CurrentThread.ManagedThreadId}"); });


            //Action<string> action = new ActionHelper().DoSomethingLong;

            //Func<string,string> funcT = new ActionHelper().DoSomethingString;

            //funcT.Invoke("测试Click");
             

            ////action.Invoke("btnAsync_Click_1");
            ////action("btnAsync_Click_1");

            ////委托自身需要的参数+2个异步参数
            ////action.BeginInvoke("btnAsync_Click_1", null, null);

            ////for (int i = 0; i < 5; i++)
            ////{
            ////    string name = string.Format($"btnAsync_Click_{i}");
            ////    //action.Invoke(name); //异步（开启一个子线程去完成）
            ////}

            //Console.WriteLine($"****************btnAsync_Click End   {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");



            //var contents = new List<Content>();
            //for (int i = 0; i < 10; i++)
            //{
            //    contents.Add(new Content { Id = i, Title = $"第{i}条数据标题", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now });
            //}  
             
            //Stopwatch sw2 = new Stopwatch(); sw2.Start();
            //Parallel.ForEach(contents, (i) => {
            //    try
            //    { 
            //        if (i.Id == 2)
            //        {
            //            throw new Exception("数据异常");
            //        }
            //        else
            //        {
            //            DoSomething2(i);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //            Console.WriteLine($"任务ID:{Task.CurrentId},线程ID:{Thread.CurrentThread.ManagedThreadId}" + "异常：" + ex.Message);
            //    }
         
            //});
            //sw2.Stop();
            //Console.WriteLine("多线程耗时：" + sw2.Elapsed.ToString());

            //ParallelLoopResult result = Parallel.For(0, 10, i =>
            //{
            //    Console.WriteLine("迭代次数：{0},任务ID:{1},线程ID:{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(10);
            //});

            //Console.WriteLine("是否完成:{0}", result.IsCompleted);
            //Console.WriteLine("最低迭代:{0}", result.LowestBreakIteration);


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
