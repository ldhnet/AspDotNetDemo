using Framework.Core.Dependency;

namespace DirectService.Test.Contracts
{
    public interface ITestInterface : IDependency
    {
        string TestFun();
    }
}
