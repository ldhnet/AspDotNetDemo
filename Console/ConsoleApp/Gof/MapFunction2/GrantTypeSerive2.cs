using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.MapFunction2
{
    public class GrantTypeSerive2
    {

        public string redPaper(string resourceId)
        {
            //红包的发放方式
            return "每周末9点发放";
        }
        public string shopping(string resourceId)
        {
            //购物券的发放方式
            return "每周三9点发放";
        }
        public string QQVip(string resourceId)
        {
            //qq会员的发放方式
            return "每周一0点开始秒杀";
        }


        public List<Func<string, string>> GetRedPaperList(string resourceId)
        {
            List<Func<string, string>> list = new List<Func<string, string>>();
            list.Add(Rules1);
            list.Add(Rules2);
            return list;
        }


        public string Rules1(string resourceId)
        {
            //红包的发放方式
            return "每周末9点发放";
        }
        public string Rules2(string resourceId)
        {
            //红包的发放方式
            return "每周末9点发放";
        }
    }
}
