using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Mediator
{
    // 具体组件房客
    class TenantComponent : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("房客没有房子住了，向中介发送求租信息。");
            this.mediator.Notify(this, "求租");
        }
        public void DoD()
        {
            Console.WriteLine("房客收到出租信息，进行相应的处理。");
        }
    }
}
