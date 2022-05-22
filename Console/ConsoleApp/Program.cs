 
using ConsoleApp.Gof.strategy;
using ConsoleApp.Observer;
using ConsoleApp.ObserverDelegate;
using ConsoleApp.Rule;
using ConsoleApp.Test202201;
using DH.Models.ExportModel;
using Framework.Utility.Excel;
using Framework.Utility.Helper;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using global::ConsoleApp.Gof.builder;
using Framework.Utility.Extensions;
using ConsoleApp.Test202203;
using ConsoleApp.Gof.Observer;
using ConsoleApp.Gof.Mediator;
using ConsoleApp.Gof.链式编程;

namespace ConsoleApp
{

    class Program
    {
        public static Func<int, int, int> fc = (int x, int y) => x * y;


        static void Main(string[] args)
        {
            //Customers.CustomerTest();

            EnumClass.EnumTest();


            // UserInforMation.Builder().SetNameAndAge("张三", 30).OutputAge().OutputName();
            UserInforMation.Builder()
                .SetName("张三")
                .SetAge(30)
                .OutputAge()
                .OutputName();

            MediatorTest.TestMain();

            Console.WriteLine(11111);

            MyAesHelper.AseTest();


            double a = 99.999933;
            var adouble = Math.Floor(a * 100) / 100;

            DateTime targetDate = new DateTime();
            DateTime.TryParseExact("202109",
                "yyyyMM",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out targetDate);

            DateTime CitationDate = DateTime.Now.AddYears(-1).AddMonths(2);
            DateTime endTime, startTime;
            //吃到书面及以上警告，自下月起不给月付              
            endTime = new DateTime(CitationDate.AddMonths(13).Year,CitationDate.AddMonths(13).Month, 1).AddDays(-1);
            startTime = new DateTime(CitationDate.AddMonths(1).Year,CitationDate.AddMonths(1).Month, 1);


           // DateTime endTime5= CitationDate.AddYears(1).LastDay();
           // DateTime startTime5 = CitationDate.AddMonths(1).FirstDay();


           // var dt11111 = new DateTime(2022,2,28);
           //var dddt= dt11111.AddDays(1).AddSeconds(-1);


           // var mainfunc = new Algorithm.MainFunction();
           // mainfunc.RedPackageHelper();



            //var aaa = (int)Math.Pow(2, 22);
            ////list 压缩
            ////Capacity 属性中的扩容机制，你只需要将Capacity设置与Count平齐，_items数组多余的虚占空间就给清掉了。
            //var list1 = Enumerable.Range(0, (int)Math.Pow(2, 2)).ToList();
            //list1.Add(1);
            //list1.Capacity = list1.Count;
            //var ppp = list1.FirstOrDefault();
            //var ppp2 = list1.LastOrDefault();

/*
            var listQQ=new List<MathProxy>();
            var ttt = listQQ.FirstOrDefault();
            if (ttt is null)
            {
                Console.WriteLine("ttt is null");
            }
            if (ttt is not null)
            {
                Console.WriteLine("ttt is null");
            }
            var dicrionary = new Dictionary<int, string>();

            DateTime expireDateTime = DateTime.MaxValue;

            var bbb22 = DateTime.Now.Date >= DateTime.Now.Date;

            var bbb11 = DateTime.Now.AddDays(-1).Date <= DateTime.Now.Date;*/

            ////concurrentDicrionary.DicrionaryTest();
            //decimal m1 = 200.01m;

            ////建造者模式
            //var person = BuilderHelper.CreatePersonBuilder
            //                .SetAge(20)
            //                .SetName("jjj")
            //                .SetGender(1)
            //                .Build();
            ////策略设计模式
            ////StrategyClass.StrategyMain();


            //int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            //int days2 = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month);

            //int days3 = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(2).Month);

            ////ExcelTest.exportExcelTest();
            ////ExcelTest.importExcelTest();

            ////Subject subject = new BankAccount(2000);
            ////subject.AddObserver(new Emailer("abcdwxc@163.com"));
            ////subject.AddObserver(new Mobile(13901234567)); 
            ////subject.WithDraw();
            ////test11111222266666
            //ObserverDelegate.Subject subject = new ObserverDelegate.Subject(2000);
            //ObserverDelegate.Emailer emailer = new ObserverDelegate.Emailer("abcdwxc@163.com");

            //ObserverDelegate.Mobile mobile = new ObserverDelegate.Mobile("15222222222");
            //subject.NotifyEvent += new NotifyEventHandler(mobile.Update);

            //subject.NotifyEvent += new NotifyEventHandler(emailer.Update);

            //subject.WithDraw();

             

            ////subject.NotifyEvent += new NotifyEventHandler(emailer.Update);
            ////subject.NotifyEvent += new NotifyEventHandler(mobile.Update);


            //IMath math =new MathExtension2();
            //MathProxy proxy = new MathProxy(math);

            //double addresult = proxy.Add(2, 3);

            ////double subresult = proxy.Sub(6, 4);

            ////double mulresult = proxy.Mul(2, 3);

            ////double devresult = proxy.Dev(2, 3);


            //var mrStartDate = Convert.ToDateTime("2021-10-30");
       
                         
            //DateTime endTime22 = new DateTime(mrStartDate.AddMonths(13).Year, mrStartDate.AddMonths(13).Month, 1).AddDays(-1);
            //DateTime startTime22 = new DateTime(mrStartDate.AddMonths(1).Year, mrStartDate.AddMonths(1).Month, 1);


            //    string str_m1 = m1.ConvertToChinese();

            //for (int i = 0; i < 10000; i++)
            //{
            //    TestStatic.GetStaticString();
            //}

            ////RedisHelper.并发测试();  
            ////RedisHelper.并发测试_未使用锁();
            ////RedisHelper.并发测试_Redis锁();
               

            //Dictionary<int, List<int>> diDic = new Dictionary<int, List<int>>();

            //var d1 = DateTime.Now.AddDays(-1).Date;

            //var d2 = DateTime.Now.Date;


            //var isbig = d1 > d2;

            //List<int> dataArr1 =new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //List<int> dataArr11 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
             

            //var dt1 = dataArr1.OrderBy(c => Guid.NewGuid());

            //var dt11 = dataArr11.OrderBy(c => Guid.NewGuid());

            //var sdt1 = string.Join(",", dt1);
            //var sdt11 = string.Join(",", dt11);

            //List<int> dataArr2 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //var guid2 = Guid.NewGuid();
            //var dt2 = dataArr2.OrderBy(c => guid2);
            //var sdt2 = string.Join(",", dt2);

            //List<int>  dataArr3 = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //var guid3 = Guid.NewGuid();

            //var dt3 = dataArr3.OrderBy(c => guid3);
            //var sdt3 = string.Join(",", dt3);

            //Console.WriteLine(sdt1);
            //Console.WriteLine(sdt11);

            //for (int i = 1; i < 5; i++)
            //{
            //    diDic.Add(i, new List<int>() { i + 1 });
            //    if (i == 2)
            //    {
            //        diDic[i].Add(i + 2);
            //    }
            //}

            //foreach (int denpendencyType in diDic.Keys)
            //{
            //    foreach (int injectType in diDic[denpendencyType])
            //    {
            //        var aa = injectType;
            //    }


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

            int[] dataArr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
     
     
    }
}
