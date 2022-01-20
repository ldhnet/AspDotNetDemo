using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.strategy
{
    public class StrategyClass
    {
        public static void StrategyMain()
        {
            Console.WriteLine("请输入通信类型：Lan、Serial");
            string input = Console.ReadLine();
            //object data = new object();
            string data = "123456789";
            Context ct = new Context();
            if (input.Equals("Lan"))  //通过客户端的选择，来确定具体用哪种通信算法
            {
                ct.SetStrategy(new Lan());
            }
            else
            {
                ct.SetStrategy(new Serial());
            }
            ct.Send(data);
            Console.ReadKey();
        }
    }
}
