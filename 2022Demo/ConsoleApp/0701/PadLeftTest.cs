using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0701
{
    public class PadLeftTest
    {
        public static void testMain()
        {
            {
                //PadLeft(Int32)
                //返回一个新字符串，该字符串通过在此实例中的字符左侧填充
                //空格来达到指定的总长度，从而实现右对齐。
                string str = "*******"; ;
                Console.WriteLine(str.PadLeft(10));
                //输出   *******
            }  
            {
                //PadLeft(Int32, Char)
                //返回一个新字符串，该字符串通过在此实例中的字符左侧填充指定的 Unicode 
                //字符来达到指定的总长度，从而使这些字符右对齐。
                string str2 = "*******"; ;
                Console.WriteLine(str2.PadLeft(10, '.'));
                //输出...*******
            }
             
            for (int i = 6; i >= 1; i--)
            {
                string strs = string.Empty.PadLeft(i, '*');
                Console.WriteLine(strs);
            } 

        }
    }
}
