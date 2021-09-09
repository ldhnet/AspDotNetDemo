using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HelperConsole.Interface;
using HelperConsole.Log;

namespace HelperConsole
{
    class Program
    {
      

        private static void SendMailUsingQueue()
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, e) =>
            { 
                int sum = 0;
                for (int i = 0; i <= 100; i++)
                {
                    sum += i;
                    Console.WriteLine($"BackgroundWorker Is:{sum}");
                }
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();
        }

        public static  DateTime LastDay(DateTime currentProcessMonth)
        {
            return currentProcessMonth.AddDays(1 - currentProcessMonth.Day).AddMonths(1).AddDays(-1).Date;//当月最后一天 
        }
        public static List<int> GetDiffAmountInvoiceList2(List<int> list, decimal _DiffAmount)
        {
            List<int> invoiceList=new List<int>();

           return GetDiffAmountInvoiceList(list, invoiceList, _DiffAmount);
        }

        public static List<int> GetDiffAmountInvoiceList(List<int> list, List<int> invoiceList, decimal _DiffAmount)
        {
             if (list.First() >= _DiffAmount)
            {
                invoiceList.Add(list.First());
            }
            else
            {
                invoiceList.Add(list.First());
                _DiffAmount = _DiffAmount - list.First();
                list.Remove(list.First());
                return GetDiffAmountInvoiceList(list, invoiceList, _DiffAmount);
            }
            return invoiceList;
        }

        static void Main(string[] args)
        {
            Assembly assem = typeof(Program).Assembly;
            Console.WriteLine("Assembly Full Name:");
            Console.WriteLine(assem.FullName);

            var d3 = DateTime.Now.AddMonths(2);
            var d4 = LastDay(d3);

            var d1= DateTime.Now.AddDays(1 - DateTime.Now.Day);

            var d2 = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1);



            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var carn = 100000*0.3m;
           var numList = new List<int> { 100000, 2000, 3000, 40000, 50000, 6000, 7000, 8000, 9000, 11000 };

            var diffCar= numList.Sum() - carn;

            var aa=   GetDiffAmountInvoiceList2(numList, carn);


            var aa222 = GetDiffAmountInvoiceList2(numList, 130000);

            var aa6 = numList.Except(aa);

            var numList1 = new List<int> { 100000, 2000, 3000, 40000, 50000, 6000, 7000, 8000, 9000, 11000 };

            var diffCar2 = numList1.Sum() - 100000 * 1.2m;
            var aa2 = GetDiffAmountInvoiceList2(numList1, diffCar2);


            var oldInvoiceList = numList1.Except(aa2).ToList();




            var numList2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aa3 = string.Join(",", GetDiffAmountInvoiceList2(numList2, 0));

            var numList3 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aa4 = string.Join(",", GetDiffAmountInvoiceList2(numList3, 8));

            var numList4 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aa1 = string.Join(",", GetDiffAmountInvoiceList2(numList4, 9));

            Console.WriteLine(aa2);
            Console.WriteLine(aa3);
            Console.WriteLine(aa4);
            Console.WriteLine(aa1);

            Parallel.For(0, nums.Length, (i) =>
            {
                Console.WriteLine($"索引：{i},数组元素：{nums[i]},线程ID：{Thread.CurrentThread.ManagedThreadId}");
            });
            //Console.ReadKey();

            List<int> nums2 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            Parallel.ForEach(nums2, (item) =>
            {
                Console.WriteLine($"输出元素：{item}、线程ID：{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.ReadKey();

            //SendMailUsingQueue();


            //var emailhelper = new EmailHelper();


            //var smtp = new SMTP("574427343@qq.com");

            //var resultE = smtp.Sendemail("请假单通知(系统邮件)", ("您好 {XXXXX}"));


            //var mailInfo = new Sys_MailInfo
            //{
            //    To = "574427343@qq.com",
            //    Subject = "LDH{邮件}通知(系统邮件)",
            //    Body = "您好 {测试测试测试得分邮件}"
            //};

            //for (int i = 0; i < 5; i++)
            //{
            //    mailInfo.AddMailToQueue();
            //}




            ////emailhelper.SentMailHXD("574427343@qq.com", "内容", "标题", "抄送", "附件（附件方法我移除了）", "你的姓名"); 

            ////for (int i = 0; i < 10; i++)
            ////{
            ////    Log($"Assembly Full Name:{i}-----{i}1111111111111111111111111111111");
            ////}
            //string result = "QQQ";
            //int timeout = 30;
            //int.TryParse(result, out timeout);

            //var aa = timeout;

            //DouDiZhuHelper douDiZhuHelper = new DouDiZhuHelper();

            //douDiZhuHelper.DouDiZhu();

            //ToByteArray(2);

            ////DelegateHelper helper = new DelegateHelper();
            ////helper.UpdateProduct_1();
            ////helper.UpdateProduct_2();

            ////LookHelper.funclookup();
            ////LookHelper.funclookup_2();

            ////SelectManyHelper.FuncSelectMany_2();

            ////FuncHelper.Delegate_1();
            ////FuncHelper.Action_1();

            ////FuncHelper.Func_1(); 

            //AssemblyName assemName = assem.GetName();
            //Console.WriteLine("\nName: {0}", assemName.Name);
            //Console.WriteLine("Version: {0}.{1}", assemName.Version.Major, assemName.Version.Minor);


            //List<int> wlist = new List<int>() {10,6, 2, 4, 8, 12 };


            //var aawlist = wlist;
            //var bbwlist = wlist;

            //aawlist.Sort();

            //bbwlist.Reverse();


            //var result1 = LoadBalance.GetMaxWeight(wlist);
            //Console.WriteLine($"\nLoadBalance1:{result1}");

            //var result2 = LoadBalance.GCD(wlist);
            //Console.WriteLine($"\nLoadBalance2:{result2}");

            //var result3 = LoadBalance.RoundRobinScheduling();
            //Console.WriteLine($"\nLoadBalance3:{result3}");
        }

        private static byte[] ToByteArray(int i)
        {
            byte[] byteArray = new byte[4];
            byteArray[0] = (byte)(i >> 24);
            byteArray[1] = (byte)((i & 0xFFFFFF) >> 16);
            byteArray[2] = (byte)((i & 0xFFFF) >> 8);
            byteArray[3] = (byte)(i & 0xFF);
            return byteArray;
        }
         
 
        private static void Log(string message)
        {
            string filePath = $"c:\\temp\\log-{DateTime.Now.ToString("yyyy-MM-dd-HH")}.txt";
            StreamWriter tw = null;
            if (File.Exists(filePath))
            {
                using (tw = File.AppendText(filePath))
                {
                    tw.WriteLine(DateTime.Now.ToString() + "> " + message);
                    tw.Flush();
                    tw.Close();
                }
            }
            else
            {
                tw = new StreamWriter(filePath);
                tw.WriteLine(DateTime.Now.ToString() + "> " + message);
                tw.Close();
            }
        }
    }
}