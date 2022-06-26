using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Business.责任链工厂.Vaildator;

namespace WebA.Business.责任链工厂.Rules
{
    public class ThreeRules : List<AbstractHandler>
    {
        public ThreeRules()
        {
            this.Add(new FirstVaildator());
            this.Add(new SecondVaildator());
            this.Add(new ThirdVaildator());
            this.Add(new FourVaildator());
        }
    }
}
