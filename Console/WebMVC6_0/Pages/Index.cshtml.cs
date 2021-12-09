using Framework.Cache;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceStack.Redis;

namespace WebMVC6_0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var kay = "KeyTest";

            var CacheValue = "CacheValue";

            CacheFactory.Cache.SetCache(kay, CacheValue);

            try
			{
                var cacheList = CacheFactory.Cache.GetCache<string>(kay);
                if (cacheList == null)
                { 
                    CacheFactory.Cache.SetCache(kay, CacheValue); 
                }
                else
                {
                    var aa= cacheList;
                }

                var qqq = CacheFactory.Cache.GetCache<string>("KeyTest");

    //            using (RedisClient client = new RedisClient("127.0.0.1", 6379))
				//{
				//	//删除当前数据库中的所有Key
				//	client.FlushDb();
				//	//删除所有数据库中的key 
				//	client.FlushAll();
				//	Console.WriteLine("存入数据");
				//	client.Set<string>("name", "vincent");
				//	Console.WriteLine("输出数据");
				//	Console.WriteLine(client.Get<string>("name"));

				//}

			}
			catch (Exception ex)
			{
				_logger.LogInformation(ex.Message);
			}

			_logger.LogInformation("11111");
        }
    }
}