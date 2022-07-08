using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0605
{
    public class yield_3_test
    {
        public void Init()
        {
            string str1 = string.Empty;
            foreach (var v1 in Power1(2, 5))
            {
                str1 += v1 + "-";
            }
            Console.WriteLine(str1.TrimEnd('-'));

            string str2 = string.Empty;
            foreach (var v2 in Power2(2, 5))
            {
                str2 += v2 + "-";
            }
            Console.WriteLine(str2.TrimEnd('-'));
        }

        private IEnumerable Power1(int num, int exp)
        {
            // yield return语句返回集合的一个元素，并移动到下一个元素上。
            //yield break可以停止迭代

            int result = 1;
            for (int i = 0; i < exp; ++i)
            {
                result = result * num;
                yield return result;
            }

            yield return 1;
            yield return 2;
            yield return 3;

            yield break;
        }
        private IEnumerable Power2(int num, int exp)
        {
            List<int> temp = new List<int>();
            int result = 1;
            for (int i = 0; i < exp; ++i)
            {
                result = result * num;
                temp.Add(result);
            }

            return temp;
        }
    }
}
