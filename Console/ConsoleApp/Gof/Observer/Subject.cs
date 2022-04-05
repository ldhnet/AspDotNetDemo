using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Observer
{
    //创建具体发布者类。
    public class Subject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Subscribe(IObserver observer)
        {
            this._observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            this._observers.Remove(observer);
        }

        public void Publish()
        {
            Console.WriteLine("商店发布优惠通知！");
            foreach (var observer in _observers)
            {
                observer.Handle(this);
            }
        }
    }
}
