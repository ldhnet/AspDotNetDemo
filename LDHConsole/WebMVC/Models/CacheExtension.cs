using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web; 
using Newtonsoft.Json;
namespace WebMVC.Models
{
    /// <summary>
    /// 缓存读写扩展
    /// </summary>
    public static class CacheExtension
    {
        static readonly ICacheHelper CacheHepler;

        static readonly string cacheType = "Memcached";//ConfigurationManager.AppSettings.Get("CacheServiceType") ?? "Default";
        static CacheExtension()
        {
            switch (cacheType)
            {
                case "Redis": 
                    break;
                case "Memcached":
                    CacheHepler = new MemcachedHelper();
                    break;
                default:
                    CacheHepler = new HttpRuntimeCacheHelper();
                    break;
            }
        }

        private static string MergeKey(string key)
        {
            return string.Format("res_hrms_{0}", key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static void ToCacheEx(this object value, string key)
        {
            if (value == null) return;
            CacheHepler.SetCache(MergeKey(key), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public static void ToHashCacheEx<T>(this List<T> value, string key, Func<T, string> getModelId)
        {
            if (value == null) return;
            CacheHepler.SetHashCache<T>(MergeKey(key), value, getModelId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="seconds"></param>
        public static void ToCacheEx(this object value, string key, int seconds)
        {
            if (value == null) return;
            CacheHepler.SetCache(MergeKey(key), value, seconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="tspan"></param>
        public static void ToCacheEx(this object value, string key, TimeSpan tspan)
        {
            if (value == null) return;
            CacheHepler.SetCache(MergeKey(key), value, tspan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object FromCacheEx(this object value, string key)
        {
            if (value == null) return null;
            return CacheHepler.GetCache(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T FromCacheEx<T>(this object value, string key)
        {
            if (value == null) return default(T);
            return CacheHepler.GetCache<T>(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return CacheHepler.GetCache(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            return CacheHepler.GetCache<T>(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetHashAll<T>(string key)
        {
            return CacheHepler.GetHashAll<T>(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            CacheHepler.RemoveCache(MergeKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RemoveAllCache()
        {
            CacheHepler.RemoveAllCache();
        }

    }

    /// <summary>
    /// 公用缓存读写接口
    /// </summary>
    public interface ICacheHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        object GetCache(string strKey);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        T GetCache<T>(string strKey);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        List<T> GetHashAll<T>(string strKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        void SetCache(string strKey, object objValue);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <param name="list"></param>
        /// <param name="getModelId"></param>
        void SetHashCache<T>(string strKey, List<T> list, Func<T, string> getModelId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        /// <param name="lNumofSeconds"></param>
        void SetCache(string strKey, object objValue, int lNumofSeconds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        /// <param name="tspan"></param>
        void SetCache(string strKey, object objValue, TimeSpan tspan);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        void RemoveCache(string strKey);

        /// <summary>
        /// 
        /// </summary>
        void RemoveAllCache();

    }

    /// <summary>
    /// HttpRuntime缓存辅助类
    /// </summary>
    public class HttpRuntimeCacheHelper : ICacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="strKey">键</param>
        public object GetCache(string strKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[strKey];
        }

        /// <summary>
        /// 获取指定返回类型的缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public T GetCache<T>(string strKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;

            object retval = objCache[strKey];
            if (retval == null || !(retval is T))
                return default(T);

            return (T)retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public List<T> GetHashAll<T>(string strKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            object retval = objCache[strKey];
            if (retval == null || !(retval is List<T>))
                return default(List<T>);
            return (List<T>)retval;
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public void SetCache(string strKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strKey, objObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <param name="list"></param>
        /// <param name="getModelId"></param>
        public void SetHashCache<T>(string strKey, List<T> list, Func<T, string> getModelId = null)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strKey, list);
        }


        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objObject"></param>
        /// <param name="seconds">缓存秒数</param>
        public void SetCache(string strKey, object objObject, int seconds)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strKey, objObject, null, DateTime.UtcNow.AddSeconds(seconds), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public void SetCache(string strKey, object objObject, TimeSpan Timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public void SetCache(string strKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(strKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public void RemoveCache(string strKey)
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            _cache.Remove(strKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }

    /// <summary>
    /// Memcached缓存辅助类
    /// </summary>
    public class MemcachedHelper : ICacheHelper
    {
        private static MemcachedClient mc = new MemcachedClient("enyim.com/memcached");
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="strKey">键</param>
        public object GetCache(string strKey)
        {
            return mc.Get(strKey);
        }

        /// <summary>
        /// 获取指定返回类型的缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public T GetCache<T>(string strKey)
        {
            return mc.Get<T>(strKey);
        }

        /// <summary>
        /// 获取多个指定KEY的缓存集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKeys"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetCache<T>(params string[] strKeys)
        {
            return mc.Get(strKeys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public List<T> GetHashAll<T>(string strKey)
        {
            return mc.Get<List<T>>(strKey); 
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        public void SetCache(string strKey, object objValue)
        {
            mc.Store(StoreMode.Add, strKey, objValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strKey"></param>
        /// <param name="list"></param>
        /// <param name="getModelId"></param>
        public void SetHashCache<T>(string strKey, List<T> list, Func<T, string> getModelId = null)
        {
            //List<HashEntry> listHashEntry = new List<HashEntry>();
            //foreach (var item in list)
            //{
            //    string json = JsonConvert.SerializeObject(item, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize, PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            //    listHashEntry.Add(new HashEntry(getModelId(item), json));
            //}
            mc.Store(StoreMode.Add, strKey, list);
        }


        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        /// <param name="lNumofSeconds">缓存秒数</param>
        public void SetCache(string strKey, object objValue, int lNumofSeconds)
        {
            mc.Store(StoreMode.Add, strKey, objValue, new TimeSpan(0, 0, lNumofSeconds));
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="objValue"></param>
        /// <param name="tspan">时间间隔</param>
        public void SetCache(string strKey, object objValue, TimeSpan tspan)
        {
            mc.Store(StoreMode.Add, strKey, objValue, tspan);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="strKey"></param>
        public void RemoveCache(string strKey)
        {
            mc.Remove(strKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAllCache()
        {
            mc.FlushAll();
        }


    }
     
}
