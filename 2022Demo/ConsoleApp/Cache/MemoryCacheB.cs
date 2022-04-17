using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Cache
{
    public class MemoryCacheB
    {

        public static void MemoryCacheTest()
        {
            IMemoryCache cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
            object result;
            string key = "KeyName";
            // 创建/更新
            result = cache.Set(key, "Testing 1");
            result = cache.Set(key, "Update 1");
            // 获取值，如果没有则返回null
            result = cache.Get(key);
            Console.WriteLine("KeyName Value=" + result);
            // 判断是否存在
            bool found = cache.TryGetValue(key, out result);
            Console.WriteLine("KeyName Found=" + result);
            // 删除
            cache.Remove(key);
            //设置项与令牌过期和回调
            TimeSpan expirationMinutes = TimeSpan.FromSeconds(0.1);
            var expirationTime = DateTime.Now.Add(expirationMinutes);
            var expirationToken = new CancellationChangeToken(
                new CancellationTokenSource(TimeSpan.FromMinutes(0.001)).Token);
            // 创建执行回调函数的缓存项
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           // 获取或设置在内存压力触发的清理过程中保留缓存中的缓存项的优先级。默认值为 Normal。
           .SetPriority(Microsoft.Extensions.Caching.Memory.CacheItemPriority.Normal)
           // 设置实际的过期时间
           .SetAbsoluteExpiration(expirationTime)
           // 获取导致缓存项过期的 IChangeToken 实例。
           .AddExpirationToken(expirationToken)
           // 获取或设置从缓存中逐出缓存项后将触发的回调。
           .RegisterPostEvictionCallback(callback: CacheItemRemoved);
            //使用回调选项添加缓存项
            result = cache.Set(key, "Call back cache Item", cacheEntryOptions);
            Console.WriteLine(result);
            Console.ReadKey();
        }
        private static void CacheItemRemoved(object key, object value, EvictionReason reason, object state)
        {
            Console.WriteLine(key + " " + value + " removed from cache due to:" + reason);
        }
    }
}
