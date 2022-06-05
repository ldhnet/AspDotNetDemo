using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0605
{
    public class TryParseTest
    { 
        public static void DateTimeTryParse()
        {
            string str1 = "2022-05-29 23:26:00";
            string str2 = "2022-05-29 23:86:00";
            DateTime dt1, dt2;
            if (DateTime.TryParse(str1, out dt1))
            {
                Console.WriteLine(dt1);//转换成功打印
            }
            if (DateTime.TryParse(str2, out dt2))
            {
                Console.WriteLine(dt2);//失败
            }
            //输出1,能转换成功，所以输出true
            Console.WriteLine(DateTime.TryParse(str1, out dt1));//true
            //输出2,转换失败，所以输出false
            Console.WriteLine(DateTime.TryParse(str2, out dt2));//false
        }
    }
}
