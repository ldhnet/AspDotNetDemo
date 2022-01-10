using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Observer
{
    public interface IObserverAccount
    {
        void Update(Subject subject);
    }
}
