using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.Mediator
{
    internal class MediatorTest
    {
        public static void TestMain()
        {
            LandlordComponent landlordComponent = new LandlordComponent();
            TenantComponent tenantComponent = new TenantComponent();
            new ConcreteMediator(landlordComponent, tenantComponent);

            landlordComponent.DoA();

            Console.WriteLine();

            tenantComponent.DoC();

            Console.ReadKey();
        }
    }
}
