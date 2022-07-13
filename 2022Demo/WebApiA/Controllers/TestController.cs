using Lee.Cache;
using Lee.Utility.Helper;
using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebA.Admin.Contracts;
using WebA.Constant;
using WebA.RpcDemo;
using WebApiA.Attributes;
using WebApiA.Code;

namespace WebApiA.Controllers
{
    [ApiController]

    [Route("api/[Controller]/[Action]")]
    public class TestController : ControllerBase
    {
        private readonly IGitHubClient _gitHubClient;
        private readonly ITestContract _testContract;
        private readonly ITokenManager _tokenManager;
        
        public TestController(IGitHubClient gitHubClient, ITestContract testContract, ITokenManager tokenManager)
        {
            _gitHubClient = gitHubClient;
            _testContract = testContract;
            _tokenManager = tokenManager;
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public async Task<IActionResult> GetDemo()
        {
            var list = _testContract.GetList();

            return Ok(list);
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns> 
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            var token = _tokenManager.Token;
            Console.WriteLine($"TestController 主线程Id === {Thread.CurrentThread.ManagedThreadId}");


            var dt=DateTime.Now;

            var tsp=  TimeStampHelper.GetTimeStamp(dt);
             
            var dtnow = TimeStampHelper.GetDateTime(tsp.ToString());
              
            return Ok(token);
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns> 
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            //var aaa = await _gitHubClient.GetData();

            var CurrentID = CacheFactory.Cache.GetCache<string>("CurrentID");

            if (string.IsNullOrEmpty(CurrentID))
            {
                CurrentID = DateTime.Now.Year.ToString();
                CacheFactory.Cache.SetCache("CurrentID", CurrentID);
            }

            return Ok(new { CurrentID });
        }
         
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpPost("PostTest")]
        [PreventDoublePost] 
        public IActionResult PostTest(int Id)
        {  
            return Ok(new { Id });
        }
    }
}
