using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0605
{
    public class yieldTest
    {
        public static void testMain()
        {
            //调用
            foreach (var item in Numbers(10))//居然返回IEnumerable
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        public static IEnumerable<int> Numbers(int n)
        {
            int current = 1, next = 1;
            for (int i = 0; i < n; ++i)
            {
                yield return current;
                //下面记住上一结果和下一个结果的计算。
                next = current + (current = next);
            }
        }
        public static IEnumerable<int> Numbers_1(int n)
        {
            int current = 1, next = 1;
            for (int i = 0; i < n; ++i)
            {
                yield return current;
                if (current == 34)
                    yield break;
                //下面记住上一结果和下一个结果的计算。
                next = current + (current = next);
            }
        }
    }
}
