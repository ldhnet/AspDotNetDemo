using System;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //BaiduAi.getImgToText();
            //PackData.Main_test();

            //PersonList p_list = new PersonList();
            //foreach (Person item in p_list)
            //{
            //    item.SayHi();
            //}
            //Console.ReadLine();

            //string aStr = "123";
            //aStr.AddMailToQueue();

            //Ref01.Main01();
            //Console.ReadKey();


            Dictionary<int,int> dic=new Dictionary<int,int>();

            for (int i = 0; i < 5; i++)
            {
                dic.Add(i,i);
            }

            foreach (var item in dic)
            {
                dic[0] += 10;
            }

            var AA = dic;
            //var a_0 = 1;
            //var a_1 = a_0++;

            //var a_2 = 1;

            //var a_3 = ++a_2;

            //var invoiceList = new List<int>();
            //var alllist = new List<int>();
            //var pagelist = new List<int>();

            //for (int k = 0; k < 10; k++)
            //    alllist.Add(k);

            //for (int k = 0; k < 5; k++)
            //    invoiceList.Add(k);

            //for (var page = 0; page < 5; ++page)
            //{
            //    pagelist.Add(page);
            //}

            //var aaaa = alllist.Except(invoiceList);

            //var lll = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddDays(-1).Date;

            //var qqqq1 = int.Parse(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));

            //var qqqq = DateTime.Now.AddMonths(51).Month.ToString().PadLeft(2, '0');

            //var ContestStartMonth1 = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //var ContestEndMonth1 = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(51).AddDays(-1).Date;

            //var StartMonth = int.Parse(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0'));
            //var PlanFinishMonth = int.Parse(DateTime.Now.AddMonths(51).Year.ToString() + DateTime.Now.AddMonths(51).Month.ToString().PadLeft(2, '0'));

            //var ContestStartMonth = new DateTime(int.Parse(StartMonth.ToString().Substring(0, 4)), int.Parse(StartMonth.ToString().Substring(4, 2)), 1);
            //var ContestEndMonth = new DateTime(int.Parse(PlanFinishMonth.ToString().Substring(0, 4)), int.Parse(PlanFinishMonth.ToString().Substring(4, 2)), 1).AddDays(-1);
            //var ContestRealEndMonth = new DateTime(int.Parse(PlanFinishMonth.ToString().Substring(0, 4)), int.Parse(PlanFinishMonth.ToString().Substring(4, 2)), 1).AddDays(-1);

            //var director = DateTime.Now;
            //int i;
            //var aa222 = int.TryParse(director.Month.ToString(), out i);
            //if (!int.TryParse(director.Month.ToString(), out i))
            //{
            //}
        }
    }
}