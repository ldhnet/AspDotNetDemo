using Framework.Core.Dependency;

namespace WebMVC.Service
{
    public interface ITestInterface: IDependency
    {
        string TestFun();
    }
}
