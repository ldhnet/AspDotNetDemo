using DirectService.Admin.Contracts;
using DirectService.Admin.Impl;
using DirectService.Test.Contracts;
using DirectService.Test.Impl;
using Framework.Utility.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace WebMVC.Controllers
{
    [AllowAnonymous]
    [SkipLoginValidate]
    public class TestController : BaseController
    {
        private readonly ITestInterface _testFace;
        private readonly ITestInterface _testFaceA;

        private readonly ITestInterface _testFaceB;

        private readonly TestInterface _testFace2 = new TestInterface();

        private readonly TestInterface2 _testFace3 = new TestInterface2();
        //接口多实现，依赖注入
        public TestController(IEnumerable<ITestInterface> testFaceList, ITestInterface testFace)
        {
            _testFace = testFace;
            _testFaceA = testFaceList.FirstOrDefault(c => c.GetType() == typeof(TestInterface));
            _testFaceB = testFaceList.FirstOrDefault(c => c.GetType() == typeof(TestInterface2));

        }

        [SkipLoginValidate]
        public IActionResult Index()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IUserService, UserService>();

            var provider = services.BuildServiceProvider();

            var singletone1 = provider.GetService<IUserService>();

            var rest6 = singletone1.Find("admin");


            var rest = _testFace.TestFun();

            var rest2 = _testFaceA.TestFun();

            var rest3 = _testFaceB.TestFun();

            var rest4 = _testFace2.TestFun();

            var rest5 = _testFace3.TestFun();

            ViewBag.Result = $"{rest}-{rest2}-{rest3}-{rest4}-{rest5}";

            return View();
        }
    }
}
