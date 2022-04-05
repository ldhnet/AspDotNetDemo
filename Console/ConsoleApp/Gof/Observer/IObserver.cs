using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Observer
{
    //声明订阅者接口。
    public interface IObserver
    {
        // 通知后处理
        void Handle(ISubject subject);
    }
}
