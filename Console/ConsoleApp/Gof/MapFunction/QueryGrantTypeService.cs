using Framework.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Gof.MapFunction
{

    public class QueryGrantTypeService
    {

        private GrantTypeSerive grantTypeSerive = new GrantTypeSerive();
        private Dictionary<string, Func<string, string>> grantTypeDic = new Dictionary<string, Func<string, string>>();

        public QueryGrantTypeService()
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
            grantTypeDic.Add("红包", resourceId=>grantTypeSerive.redPaper(resourceId));
            grantTypeDic.Add("购物券", resourceId => grantTypeSerive.shopping(resourceId));
            grantTypeDic.Add("qq会员", resourceId => grantTypeSerive.QQVip(resourceId));
        }

        public string getResult(string resourceType)
        {
            string resourceId = "红包";
            //Controller根据 优惠券类型resourceType、编码resourceId 去查询 发放方式grantType
            Func<string, string> result = grantTypeDic.Get(resourceType);
            if (result != null)
            {
                //传入resourceId 执行这段表达式获得String型的grantType
                return result.Invoke(resourceId);
            }
            return "查询不到该优惠券的发放方式";
        }
    }
}
