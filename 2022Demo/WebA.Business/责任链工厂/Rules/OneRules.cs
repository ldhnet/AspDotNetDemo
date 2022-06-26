using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Business.责任链工厂.Vaildator;

namespace WebA.Business.责任链工厂.Rules
{
    public class OneRules : List<AbstractHandler>
    {
        public OneRules()
        {
            this.Add(new FirstVaildator()); 
        }
    }
}
