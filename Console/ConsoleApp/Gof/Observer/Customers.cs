using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Observer
{

    public class Customers
    {
        public static void CustomerTest()
        {
            var subject = new Subject();

            var observerA = new CustomerA();
            subject.Subscribe(observerA);
            var observerB = new CustomerB();
            subject.Subscribe(observerB);

            subject.Publish();

            Console.WriteLine();

            subject.Unsubscribe(observerB);
            subject.Publish();

            Console.ReadKey();
        }
    }


    //具体订阅者类中实现通知后处理的方法。
    public class CustomerA : IObserver
    {
        public void Handle(ISubject subject)
        {
            Console.WriteLine("顾客A收到优惠通知。");
        }
    }


    public class CustomerB : IObserver
    {
        public void Handle(ISubject subject)
        {
            Console.WriteLine("顾客B收到优惠通知。");
        }
    }
}
