using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Business.责任链工厂.Vaildator
{
    public class ThirdVaildator : AbstractHandler
    {
        private int play()
        {
            return 95;
        }

        public override RuleResult handler()
        {
            Console.WriteLine("第三关-->SecondPassHandler");

            var ruleResult = new RuleResult();

            int score = play();
            if (score >= 95)
            {
                //分数>=95 并且存在下一关才进入下一关
                if (this.next != null)
                {
                    return this.next.handler();
                }
            }
            else
            {
                ruleResult.Success = false;
                ruleResult.Msg = "验证失败";
            } 
            return ruleResult;
        }
    }
}
