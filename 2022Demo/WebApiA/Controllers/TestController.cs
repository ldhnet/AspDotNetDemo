using Lee.Cache;
using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebA.Admin.Contracts;
using WebA.Constant;
using WebA.RpcDemo;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IGitHubClient _gitHubClient;
        //private readonly ITestContract _testContract;, ITestContract testContract
        public TestController(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
            //_testContract = testContract;
        }
        ///// <summary>
        ///// 测试demo
        ///// </summary>
        ///// <returns></returns> 
        //[HttpGet]
        //public async Task<IActionResult> GetDemo()
        //{
        //    //var list = _testContract.GetList();

        //    return Ok(1);
        //}

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
