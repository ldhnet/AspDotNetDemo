using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Observer
{
   public class Mobile : IObserverAccount
    {
        private long _phoneNumber;        
        public Mobile(long phoneNumber)
        {
            this._phoneNumber = phoneNumber;            
        }
        public void Update(Subject subject)
        {
            Console.WriteLine("Notified :Phone number is {0} You withdraw  {1:C} ", _phoneNumber, subject.Money);
        }
    }
}
