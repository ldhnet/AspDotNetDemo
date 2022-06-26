using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Business.责任链
{
    public class RuleResult
    {
        public RuleResult()
        { 
            Success = true;
        }
        public bool Success { get; set; }
        public string Msg { get; set; }
    }
}
