using Microsoft.Extensions.Caching.Memory;
using System;

namespace WebMVC.Helper
{
    public class MemoryCacheProvider
    {    //外面可以直接调它
        public MemoryCache MemoryCache { get; set; }

        public MemoryCacheProvider()
        {
            MemoryCacheOptions cacheOps = new MemoryCacheOptions()
            {
                //缓存最大为1024份
                //##注意netcore中的缓存是没有单位的，缓存项和缓存的相对关系
                SizeLimit = 1024,

                //缓存满了时，压缩20%（即删除20份优先级低的缓存项）
                CompactionPercentage = 0.2,

                //10 * 60 秒钟查找一次过期项
                ExpirationScanFrequency = TimeSpan.FromSeconds(10 * 60)
            };
            MemoryCache = new MemoryCache(cacheOps);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return (T)MemoryCache.Get(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="createItem"></param>
        /// <returns></returns>
        public T Set<T>(object key, T createItem)
        {
            var options = new MemoryCacheEntryOptions()
            {
                //缓存大小占1份
                Size = 1,
                //优先级，当缓存压缩时会优先清除优先级低的缓存项
                Priority = CacheItemPriority.Low,//Low,Normal,High,NeverRemove
                //相对过期时间
                SlidingExpiration = TimeSpan.FromSeconds(2),
                //绝对过期时间1
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),

                //绝对过期时间2
                //AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(2)),  
            };

            return MemoryCache.Set(key, createItem, options);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveCache(object key)
        {
            MemoryCache.Remove(key);
        }

        public T GetOrCreate<T>(object key, Func<T> createItem)
        {
            T cacheEntry;
            if (!MemoryCache.TryGetValue(key, out cacheEntry))// Look for cache key.
            {
                // Key not in cache, so get data.
                cacheEntry = createItem();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSize(1)//Size amount
                            //Priority on removing when reaching size limit (memory pressure)
                    .SetPriority(CacheItemPriority.Normal)
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(2))
                    // Remove from cache after this time, regardless of sliding expiration
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                // Save data in cache.
                MemoryCache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }
    }
}
