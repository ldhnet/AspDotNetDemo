using Lee.Utility.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics; 
using WebA.Models;

namespace web6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DistributedRedisCache _Cache;
        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _Cache = new DistributedRedisCache(cache);
        }

        public IActionResult Index()
        {

            string val = HttpContext.Session.GetString("test_username");
            //session为空写入一条测试数据
            if (string.IsNullOrEmpty(val))
            {
                HttpContext.Session.SetString("test_username", "a1111111111");
            }

            ViewBag.sessionid = HttpContext.Session.Id;
            ViewBag.val = val;

            return View();
        }

        public IActionResult Privacy()
        {
            //添加
            bool booladd = _Cache.Set("id", "sssss");
            //验证
            bool boolExists = _Cache.Exist("id");
            //获取
            object obj = _Cache.Get("id");
            //删除
            //bool boolRemove = _Cache.Remove("id");
            //修改
            bool boolModify = _Cache.Modify("id", "ssssssss");

            ViewBag.Id = obj;
            //bool isExisted;
            //isExisted = _Cache.Exist("abc");//查询键值"abc"是否存在
            //_Cache.Remove("abc");//删除不存在的键值"abc"，不会报错

            //string key = "Key01";//定义缓存键"Key01"
            //string value = "This is a demo key !";//定义缓存值

            //_Cache.Set(key, value, new DistributedCacheEntryOptions()
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            //});//设置键值"Key01"到Redis，使用绝对过期时间，AbsoluteExpirationRelativeToNow设置为当前系统时间10分钟后过期

            ////也可以通过AbsoluteExpiration属性来设置绝对过期时间为一个具体的DateTimeOffset时间点
            ////redisCache.Set(key, value, new DistributedCacheEntryOptions()
            ////{
            ////    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10)
            ////});//设置键值"Key01"到Redis，使用绝对过期时间，AbsoluteExpiration设置为当前系统时间10分钟后过期

            //var getVaue = _Cache.Get<string>(key, out isExisted);//从Redis获取键值"Key01"，可以看到getVaue的值为"This is a demo key !"

            //value = "This is a demo key again !";//更改缓存值

            //_Cache.Set(key, value, new DistributedCacheEntryOptions()
            //{
            //    SlidingExpiration = TimeSpan.FromMinutes(10)
            //});//将更改后的键值"Key01"再次缓存到Redis，这次使用滑动过期时间，SlidingExpiration设置为10分钟

            //getVaue = _Cache.Get<string>(key, out isExisted);//再次从Redis获取键值"Key01"，可以看到getVaue的值为"This is a demo key again !"

            //_Cache.Remove(key);//从Redis中删除键值"Key01"


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}