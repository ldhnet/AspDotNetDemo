using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ShuffleCopyHelper
    {
       public  T[] ShuffleCopy<T>(IEnumerable<T> data, Random r)
        {
            var arr = data.ToArray();

            for (var i = arr.Length - 1; i > 0; --i)
            {
                int randomIndex = r.Next(i + 1);

                T temp = arr[i];
                arr[i] = arr[randomIndex];
                arr[randomIndex] = temp;
            } 
            return arr;
        }
        public void LuckDraw(IEnumerable<T> data, int maxTime,int tatal = 1)
        { 
            
        }
        
       public int[] Measure(int n, int maxTime)
        {
            var data = Enumerable.Range(0, n);
            var sum = new int[n];

            var r = new Random();
            for (var times = 0; times < maxTime; ++times)
            {
                var result = ShuffleCopy(data, r);
                for (var i = 0; i < n; ++i)
                {
                    sum[i] += result[i] != i ? 1 : 0;
                }
            }
            return sum;
        }

        public T[] ShuffleCopy2<T>(IEnumerable<T> data, Random r)
        { 
            return data.OrderBy(v => r.NextDouble() < 0.5).ToArray();
        }
    }
}
