using Microsoft.Extensions.Caching.Memory;

namespace Framework.Utility.Helper
{
    public class MemoryCacheHelper
    {
        /// <summary>
        /// 简单缓存实现
        /// </summary>
        ///   var _avatarCache = new SimpleMemoryCache<byte[]>();...
        //    var myAvatar = _avatarCache.GetOrCreate(userId, () => _database.GetAvatar(userId));
        /// <typeparam name="TItem"></typeparam>
        public class SimpleMemoryCache<TItem>
        {
            private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

            public TItem GetOrCreate(object key, Func<TItem> createItem)
            {
                TItem cacheEntry;
                if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
                {
                    // Key not in cache, so get data.
                    cacheEntry = createItem();

                    // Save data in cache.
                    _cache.Set(key, cacheEntry);
                }
                return cacheEntry;
            }
        }

        /// <summary>
        /// 具有驱逐策略的 IMemoryCache
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        public class MemoryCacheWithPolicy<TItem>
        {
            /*
              1.SizeLimit被添加到MemoryCacheOptions.这为我们的缓存容器添加了基于大小的策略。大小没有单位。相反，我们需要在每个缓存条目上设置大小数量。在这种情况下，我们每次将金额设置为 1 SetSize(1)。这意味着缓存限制为 1024 个项目。
              2.当我们达到大小限制时，应该删除哪个缓存项？您实际上可以使用.SetPriority(CacheItemPriority.High). 级别为Low、Normal、High和NeverRemove。
              3.SetSlidingExpiration(TimeSpan.FromSeconds(2))添加了，它将滑动过期时间设置为 2 秒。这意味着如果一个项目在 2 秒内未被访问，它将被删除。
              4.SetAbsoluteExpiration(TimeSpan.FromSeconds(10))添加了，将绝对过期时间设置为 10 秒。这意味着该项目将在 10 秒内被驱逐，如果它还没有。
            */
            private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 1024
            });

            public TItem GetOrCreate(object key, Func<TItem> createItem)
            {
                TItem cacheEntry;
                if (!_cache.TryGetValue(key, out cacheEntry))// Look for cache key.
                {
                    // Key not in cache, so get data.
                    cacheEntry = createItem();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetSize(1)//Size amount
                                //Priority on removing when reaching size limit (memory pressure)
                        .SetPriority(CacheItemPriority.High)
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(2))
                        // Remove from cache after this time, regardless of sliding expiration
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                    // Save data in cache.
                    _cache.Set(key, cacheEntry, cacheEntryOptions);
                }
                return cacheEntry;
            }
        }


    }
}
