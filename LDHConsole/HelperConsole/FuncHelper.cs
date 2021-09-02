using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperConsole
{
   public static class FuncHelper
    {
        protected delegate int ClassDelegate(int x, int y);//定义委托类型及参数


        public static void Delegate_1()
        {
            ClassDelegate dele = new ClassDelegate(Add);//实例化一个委托

            var result_1 = dele(1, 2);
            Console.WriteLine(result_1);//调用委托
            Console.ReadKey();
        }

        private static int Add(int a, int b)
        {
            return a + b;
        }



        public static void Action_1()//没有返回值的委托
        {
            Action<int, int> ac = new Action<int, int>(ShowAddResult);//实例化一个委托
            
            ac(1, 2);//调用委托

            Action<int, int,string> ac2 = new Action<int, int,string>(ShowAddResult2);//实例化一个委托

            ac2(1, 2,"3");//调用委托

            Action<int, int,string> ac3 = ((p, q,k) => Console.WriteLine(p + q + k));//实例化一个委托

            ac2(1, 2, "3");//调用委托
             

            Console.ReadKey();
        }
      
        public static void Action_2()//lambda表达式委托
        {
            Action<int, int> ac = ((p, q) => Console.WriteLine(p + q));//实例化一个委托
            ac(1, 2);//调用委托

            Console.ReadKey();
        }

        private static void ShowAddResult2(int a, int b,string c)
        {
            var cc = int.Parse(c);
            Console.WriteLine(a + b + cc);
        }

        private static void ShowAddResult(int a, int b)
        {
            Console.WriteLine(a + b);
        }




        public static void Func_1()
        {
            Func<string,string> fc1 = new Func<string,string>(ShowAddResult3);//实例化一个委托
            string result = fc1("qqqqqqq调用委托");//调用委托
            string result2 = fc1("qqqqqqq调用委托2222");//调用委托
            Console.WriteLine(result);

             

            Func<int,int,int> fc2 = new Func<int, int, int>(TestAdd);
            var result_3 = fc2(6,2);
            Console.WriteLine(result_3);

            Func<int, int,int, string> fc3= new Func<int, int, int,string>(ShowAddResult4);
            var result_4 = fc3(2,2,2);

            Console.WriteLine(result_4);


            //Console.ReadKey();

        }
        static int TestAdd(int a,int b)
        {
            return a + b;
        }
        private static string ShowAddResult4(int a,int b, int c)
        {
            var cc=(a+b+c).ToString();
            return cc;
        }


        private  static string ShowAddResult3(string param)
        {
            return param;
        }


        static void Func_2()//lambda表达式委托
        {
            //实例化一个委托,注意不加大括号，写的值就是返回值，不能带return
            Func<string> fc1 = () => "地球是圆的";

            //实例化另一个委托,注意加大括号后可以写多行代码，但是必须带return
            Func<string> fc2 = () =>
            {
                return "地球是圆的";
            };

            string result = fc1();//调用委托
            string result2 = fc2();//调用委托

            Console.WriteLine(result);
            Console.WriteLine(result2);
            Console.ReadKey();
        }


        static void Func_3()
        {
            //实例化一个委托,注意不加大括号，写的值就是返回值，不能带return
            Func<int, string> fc1 = (p) => "传入参数" + p + ",地球是圆的";

            //实例化另一个委托,注意加大括号后可以写多行代码，但是必须带return
            Func<string, string> fc2 = (p) =>
            {
                return "传入参数" + p + ",地球是圆的";
            };

            string result = Test<int>(fc1, 1);//调用委托
            string result2 = Test<string>(fc2, "1");//调用委托

            Console.WriteLine(result);
            Console.WriteLine(result2);
            Console.ReadKey();
        }

        static string Test<T>(Func<T, string> fc, T inputParam)
        {
            return fc(inputParam);
        }

    }
}
