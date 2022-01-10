using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ConsoleApp.ObserverDelegate
{
    public class Subject
    { 
        public event NotifyEventHandler NotifyEvent;

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
            OnNotifyChange();
        }
        public void OnNotifyChange()
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this);
            }
        }

    }
}
