using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Business.责任链
{
    public abstract class AbstractHandler
    {
        protected AbstractHandler next;

        public void setNext(AbstractHandler next)
        {
            this.next = next;
        }

        public abstract RuleResult handler();
    } 
}
