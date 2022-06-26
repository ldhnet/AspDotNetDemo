using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebA.Business.责任链工厂.Rules;

namespace WebA.Business.责任链工厂
{
    public class RuleProcess
    {
        public List<RuleResult> Exectue(int type)
        {
            var ruleList = new List<RuleResult>();
            var Rules = this.RulesFactory(type);
            Rules.ForEach((u) =>
            {
                var rule = u.handler();
                ruleList.Add(rule);
            });
            return ruleList;
        }

        private List<AbstractHandler> RulesFactory(int type)
        {
            switch (type)
            {
                case 1: return new OneRules();
                case 2: return new TwoRules();
                case 3: return new ThreeRules();
            }
            return null;
        }
    }
}
