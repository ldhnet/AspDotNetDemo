using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Observer
{
    public class Emailer : IObserverAccount
    {
        private string _emalier;
        public Emailer(string emailer)
        {
            this._emalier = emailer;
        }
        public void Update(Subject subject)
        {
            Console.WriteLine("Notified : Emailer is {0}, You withdraw  {1:C} ", _emalier, subject.Money);
        }
    }
}
