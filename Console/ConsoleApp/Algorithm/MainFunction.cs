using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Algorithm
{
    public class MainFunction
    {
        /// <summary>
        /// 每次抢到的金额 = 随机区间 （0， M / N × 2）
        /// </summary>
        public void RedPackageHelper()
        { 
            for (int i = 0; i < 10; i++)
            {
                //var list = RedPackage.DivideRedPackage(2, 10);

                var list = RedPackage.DivideRedPackage(100 * 100, 10);
                Console.WriteLine(string.Join(",", list));
                int count = 0;
                foreach (var item in list)
                {
                    count += item;
                }
                Console.WriteLine(count);
            }
            System.Console.ReadKey();
        }
        /// <summary>
        /// 手速版
        /// </summary>
        public void SpeedRedPackageHelper()
        {
            for (int i = 0; i < 10; i++)
            {
                var list = SpeedRedPackage.DivideRedPackage(100 * 100, 10);
                Console.WriteLine(string.Join(",", list));
                int count = 0;
                foreach (var item in list)
                {
                    count += item;
                }
                Console.WriteLine(count);
            }
            System.Console.ReadKey();
        }

    }
}
