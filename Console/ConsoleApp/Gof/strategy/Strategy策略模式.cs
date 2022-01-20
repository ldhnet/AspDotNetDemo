using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.strategy
{
    public interface ICommunication
    {
        bool Send(object data);
    }

    public class Serial : ICommunication
    {
        public bool Send(object data)
        {
            Console.WriteLine("通过串口发送一个数据的算法");
            return true;
        }
    }

    public class Lan : ICommunication
    {
        public bool Send(object data)
        {
            var str=data.ToString();

            Console.WriteLine($"通过网口发送一个数据的算法{data}__{str}");
            return true;
        }
    }

    public class Context
    {
        private ICommunication _communication;
        public void SetStrategy(ICommunication communication)//传递具体的策略
        {
            this._communication = communication;
        }
        public bool Send(object data)
        {
            return this._communication.Send(data);
        }
    }

}
