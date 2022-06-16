using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0616
{
    public class ClosureTests
    {
        public static void TestMain()
        {
            Console.WriteLine("******************CreateActions***********************");
            var actions=CreateActions();
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i]();
            }
            Console.WriteLine("******************CreateActions_1***********************");
            var actions_1 = CreateActions_1();
            for (int i = 0; i < actions_1.Count; i++)
            {
                actions_1[i]();
            }
            Console.WriteLine("******************CreateActions_2***********************");
            var actions_2 = CreateActions_2();
            for (int i = 0; i < actions_2.Count; i++)
            {
                actions_2[i]();
            }
        }

        /// <summary>
        /// 出现闭包 产生bug   Console.WriteLine(i) 打印的值相同
        /// </summary>
        /// <returns></returns>
        private static List<Action> CreateActions()
        { 
            var actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                actions.Add(() => Console.WriteLine(i));
            }
            return actions;
        }

        /// <summary>
        /// 闭包问题
        /// 解决方式一：添加局部变量，让每个action 执行自己的 局部变量
        /// </summary>
        /// <returns></returns>

        private static List<Action> CreateActions_1()
        {
            var actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                int temp=i;
                actions.Add(() => Console.WriteLine(temp));
            }
            return actions;
        }
        /// <summary>
        /// 闭包问题
        /// 解决方式二：使用Foreach 循环
        /// </summary>
        /// <returns></returns>

        private static List<Action> CreateActions_2()
        {
            var actions = new List<Action>();
            foreach (var i in Enumerable.Range(0,5))
            { 
                actions.Add(() => Console.WriteLine(i));
            }
            return actions;
        }
    }
}
