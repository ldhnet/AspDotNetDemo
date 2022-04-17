using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Cache
{
    public class MemoryCacheA
    {
        public static void MemoryCacheTest()
        {
            ObjectCache cache = MemoryCache.Default;
            //添加缓存
            //null是不指定缓存策略，CacheItemPolicy可用于添加缓存过期时间、更改监视器、更新回调等。
            cache.Add("CacheName", "Value1", null);
            cache.Add("CacheName2", 100, null);
            // 创建缓存的策略
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30.0),
            };
            //用缓存项策略添加缓存
            cache.Add("CacheName3", "Expires In A Minute", cacheItemPolicy);
            //使用CacheItem对象添加缓存
            var cacheItem = new CacheItem("fullName", "Vikas Lalwani");
            cache.Add(cacheItem, cacheItemPolicy);
            //获取缓存值并打印
            Console.WriteLine("Full Name " + cache.Get("fullName"));
            //打印所有缓存
            Console.WriteLine("更新前");
            PrintAllCache(cache);
            //删除缓存
            cache.Remove("CacheName");
            //更新缓存值
            cache.Set("CacheName2", 2000, null);
            //更新后输出所有缓存
            Console.WriteLine("更新后");
            PrintAllCache(cache);
        }
        private static void PrintAllCache(ObjectCache cache)
        {
            foreach (var item in cache)
            {
                Console.WriteLine("cache object key-value: " + item.Key + "-" + item.Value);
            }
        }
    }
}
