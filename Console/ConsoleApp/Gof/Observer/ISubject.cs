using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Observer
{
    public interface ISubject
    {
        // 订阅
        void Subscribe(IObserver observer);

        // 取消订阅
        void Unsubscribe(IObserver observer);

        // 发布
        void Publish();
    }
}
