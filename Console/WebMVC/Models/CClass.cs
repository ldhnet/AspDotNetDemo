
using DirectService.Test.Contracts;

namespace WebMVC.Models
{
    public class CClass : BClass, ITestInterface
    {
        public int C_StartEngine()
        {
            //启动发动机的代码
            return 0;
        }

        public int C_StopEngine()
        {
            //停止发动机的代码
            return 0;
        }
        public string TestFun()
        {
            return "789";
        }
    }
}
