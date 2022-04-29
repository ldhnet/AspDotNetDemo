using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Test202203
{
    public class EnumClass {

        public static void EnumTest()
        {
            var status = Jod.Athletes | Jod.Teacher;

            Console.WriteLine(status);

            // 2.判断某个人的职业中是否有Athletes
            if ((status & Jod.Athletes) == Jod.Athletes)
            {
                // 是运动员
            }
            // 2.判断某个人的职业中是否有Athletes
            if ((status & Jod.Athletes) == Jod.Athletes)
            {
                // 是运动员
            }
            // 2.判断某个人的职业中是否有Athletes
            if ((status & Jod.Athletes) == Jod.Athletes)
            {
                // 是运动员
            }
        }

    }

    [Flags]
    public enum Jod
    {
        /// <summary>
        /// 老师
        /// </summary>
        Teacher = 1,

        /// <summary>
        /// 运动员
        /// </summary>
        Athletes = 2
    }

}
