﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Business.责任链工厂.Vaildator
{
    public class FourVaildator : AbstractHandler
    {
        private int play()
        {
            return 100;
        }

        public override RuleResult handler()
        {
            Console.WriteLine("第四关-->SecondPassHandler");
            RuleResult ruleResult = new RuleResult();
            int score = play();
            if (score >= 100)
            {
                //分数>=100 并且存在下一关才进入下一关
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
