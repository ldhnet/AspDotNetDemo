using DirectService.Test.Contracts;

namespace DirectService.Test.Impl
{
    public class TestInterface : ITestInterface
    {
        public string TestFun()
        {
            return "123456";
        }
    }
}
