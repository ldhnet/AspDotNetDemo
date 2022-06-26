using Lee.Utility.Helper;
using WebA.Business.责任链.Vaildator;
using WebA.Business.责任链工厂;
using WebA.Business.责任链工厂.Rules;

namespace WebA.Business
{
    public class Test
    {

        #region 责任链
        public void 责任链start()
        {
            FirstVaildator firstPassHandler = new FirstVaildator();//第一关
            SecondVaildator secondPassHandler = new SecondVaildator();//第二关
            ThirdVaildator thirdPassHandler = new ThirdVaildator();//第三关

            FourVaildator fourPassHandler = new FourVaildator();//第四关

            // 和上面没有更改的客户端代码相比，只有这里的set方法发生变化，其他都是一样的
            firstPassHandler.setNext(secondPassHandler);//第一关的下一关是第二关
            secondPassHandler.setNext(thirdPassHandler);//第二关的下一关是第三关
            thirdPassHandler.setNext(fourPassHandler);

            //说明：因为第三关是最后一关，因此没有下一关

            //从第一个关卡开始
            var result = firstPassHandler.handler();
        }

        #endregion


        #region 责任链工厂

        public void 责任链工厂Start()
        {
            RuleProcess process = new RuleProcess();
            var list = process.Exectue(1);

            Console.WriteLine("**************list*************");
            Console.WriteLine(JsonHelper.ToJson(list));

            var list2 = process.Exectue(2);

            Console.WriteLine("**************list2*************");
            Console.WriteLine(JsonHelper.ToJson(list2));

            var list3 = process.Exectue(3);

            Console.WriteLine("**************list3*************");
            Console.WriteLine(JsonHelper.ToJson(list3));
        }
  
        #endregion

    }
}