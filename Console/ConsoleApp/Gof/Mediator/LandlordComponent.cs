using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Mediator
{
    // 4. 具体组件房东
    class LandlordComponent : BaseComponent
    {
        public void DoA()
        {
            Console.WriteLine("房东有房子空出来了，向中介发送出租信息。");
            this.mediator.Notify(this, "出租");
        }
        public void DoB()
        {
            Console.WriteLine("房东收到求租信息，进行相应的处理。");
        }
    }
}
