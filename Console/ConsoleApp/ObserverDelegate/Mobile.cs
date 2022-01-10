using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ObserverDelegate
{
    public class Mobile
    {
        private string _mobile;
        public Mobile(string mobile)
        {
            this._mobile = mobile;
        }
        public void Update(object obj)
        {
            if (obj is Subject)
            {
                Subject subject = (Subject)obj;

                Console.WriteLine("Notified : Mobile is {0}, You withdraw  {1:C} ", _mobile, subject.Money);
            }
        }
    }
}
