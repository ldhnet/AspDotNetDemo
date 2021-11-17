using DirectService.Test.Contracts;

namespace DirectService.Test.Service
{
    public class TestInterface : ITestInterface
    {
        public string TestFun()
        {
            return "123456";
        }
    }
}
