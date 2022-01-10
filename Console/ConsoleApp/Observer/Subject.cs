using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Observer
{
    public abstract class Subject
    {
        private List<IObserverAccount> Observers = new List<IObserverAccount>();

        private double _money;
        public Subject(double money)
        {
            this._money = money;
        }

        public double Money
        {
            get { return _money; }
        }

        public void WithDraw()
        {
            foreach (IObserverAccount ob in Observers)
            {
                ob.Update(this);
            }
        }
        public void AddObserver(IObserverAccount observer)
        {
            Observers.Add(observer);
        }
        public void RemoverObserver(IObserverAccount observer)
        {
            Observers.Remove(observer);
        }

    }
}
