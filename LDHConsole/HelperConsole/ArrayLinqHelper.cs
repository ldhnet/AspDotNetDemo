using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{
   public class ArrayLinqHelper
    {
        public void FuncArray()
        { 
            List<object> list = new List<object>();
            list.Add("Power");
            list.Add("Power1");
            list.Add("Aower2");
            list.Add("Bower3");
            list.Add("Bower5");
            list.Add(1);
            list.Add(DateTime.Now);


            var query = list.OfType<string>().Where(p => p.StartsWith("P")).ToList(); //输出   Power 

            var query2 = list.OfType<string>().ToList(); //输出   Power 

            int[] arr = { 1, 4, 8, 2, 3, 1 };



            foreach (var item in arr.Reverse())
{
                Console.WriteLine(item);
            }


            var query3 = arr.TakeWhile(x => x <= 5); //输出 1 4
            var query4 = arr.SkipWhile(x => x <= 5);//输出   8 2 3 1



            int[] arr_5 = { };
            var query_5 = arr_5.DefaultIfEmpty();//输出  0

            var query_6 = arr.DefaultIfEmpty();//输出  0



            int[] arr1 = { 1, 2, 5, 5, 3 };
            int[] arr2 = { 2, 5, 6 };

            var query_7 = arr1.Intersect(arr2);
            var query_8 = arr1.Except(arr2);
            var query_8_1 = arr2.Except(arr1);
            var query_9 = arr1.Concat(arr2);
            var query_10 = arr1.Union(arr2);


            int[] arr11 = { 1, 2, 3, 4 };
            string[] arr12 = { "一", "二", "三", "四", "五" };

            var query_11 = arr11.Zip(arr12, (x, y) => string.Format("{0},{1}", x, y));
              
        }
    }
}
