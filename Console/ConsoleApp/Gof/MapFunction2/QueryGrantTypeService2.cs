using Framework.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.MapFunction2
{

    public class QueryGrantTypeService2
    {

        private GrantTypeSerive2 grantTypeSerive = new GrantTypeSerive2();
        private Dictionary<string, Func<string, List<Func<string, string>>>> grantTypeDic = new Dictionary<string, Func<string, List<Func<string, string>>>>();

        public QueryGrantTypeService2()
        {
            dispatcherInit();
        }

        /**
         *  初始化业务分派逻辑,代替了if-else部分
         *  key: 优惠券类型
         *  value: lambda表达式,最终会获得该优惠券的发放方式
         */
        public void dispatcherInit()
        {
            grantTypeDic.Add("红包", resourceId => grantTypeSerive.GetRedPaperList(resourceId));
            //grantTypeDic.Add("购物券", resourceId => grantTypeSerive.shopping(resourceId));
            //grantTypeDic.Add("qq会员", resourceId => grantTypeSerive.QQVip(resourceId));
        }

        public List<string> getResult(string resourceType)
        {
            string resourceId = "红包";
            List<string> resultList=new List<string>();
            Func<string, List<Func<string, string>>> result = grantTypeDic.Get(resourceType);
            if (result != null)
            {
                //传入resourceId 执行这段表达式获得String型的grantType
                var funcList= result.Invoke(resourceId);
                foreach (var item in funcList)
                {
                    resultList.Add(item.Invoke(resourceId));
                }
            }
            return resultList;
        }
    }
}
