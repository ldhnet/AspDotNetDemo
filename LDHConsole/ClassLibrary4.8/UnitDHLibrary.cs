using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary4._8
{
    public class UnitDHLibrary
    {
        public int addTest(int a, int b)
        {
            int c = a + b;
            return c;
        }
        /// <summary>
        /// 获取三角形类型.
        /// </summary>
        /// <param name="sideArr">三角形三边长度数组.</param>
        /// <returns>返回三角形类型名称.</returns>
        public static string GetTriangle(string[] sideArr)
        {
            string result = string.Empty;
            int a = int.Parse(sideArr[0]);
            int b = int.Parse(sideArr[1]);
            int c = int.Parse(sideArr[2]);
            if (a + b > c && a + c > b && b + c > a)
            {
                if (a == b && a == c)
                {
                    result = "等边三角形";
                }

                if (a == b || a == c || b == c)
                {
                    result = "等腰三角形";
                }
                else
                {
                    result = "一般三角形";
                }
            }
            else
            {
                result = "不构成三角形";
            }
            return result;
        }
    }
}
