﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebA.Business.责任链.Vaildator
{
    public class FirstVaildator : AbstractHandler
    { 
        private int play()
        {
            return 80;
        }
         
        public override RuleResult handler()
        {
            Console.WriteLine("第一关-->FirstPassHandler");
            RuleResult ruleResult = new RuleResult();
            int score = play();
            if (score >= 80)
            {
                //分数>=80 并且存在下一关才进入下一关
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
