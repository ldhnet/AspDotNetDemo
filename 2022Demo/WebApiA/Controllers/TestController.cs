using Lee.Cache;
using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebA.Admin.Contracts;
using WebA.Constant;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    { 
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpGet("Demo")]
        public IActionResult Get()
        {
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
