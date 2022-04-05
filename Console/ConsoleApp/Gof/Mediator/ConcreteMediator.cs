using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Mediator
{
    class ConcreteMediator : IMediator
    {
        private readonly LandlordComponent landlordComponent;
        private readonly TenantComponent tenantComponent;

        public ConcreteMediator(LandlordComponent landlordComponent, TenantComponent tenantComponent)
        {
            this.landlordComponent = landlordComponent;
            this.landlordComponent.SetMediator(this);

            this.tenantComponent = tenantComponent;
            this.tenantComponent.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "求租")
            {
                Console.WriteLine("中介收到求租信息后通知房东。");
                landlordComponent.DoB();
            }
            if (ev == "出租")
            {
                Console.WriteLine("中介收到出租信息后通知房客。");
                tenantComponent.DoD();
            }
        }
    }
}
