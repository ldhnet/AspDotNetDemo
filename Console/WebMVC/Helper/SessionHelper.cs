using Framework.Utility.Config;
using Framework.Utility.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace WebMVC.Helper
{
    public class SessionHelper
    {
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void SetSession<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            IHttpContextAccessor hca = GlobalConfig.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void SetSession(string key, string value)
        {
            SetSession<string>(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public static string GetSession(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            IHttpContextAccessor hca = GlobalConfig.ServiceProvider?.GetService<IHttpContextAccessor>();
            return hca?.HttpContext?.Session.GetString(key) as string;
        }


        public static T GetSession<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }
            IHttpContextAccessor hca = GlobalConfig.ServiceProvider?.GetService<IHttpContextAccessor>();
            string retval = hca?.HttpContext?.Session.GetString(key) as string;
            return JsonHelper.FromJson<T>(retval);
        }


        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void RemoveSession(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            IHttpContextAccessor hca = GlobalConfig.ServiceProvider?.GetService<IHttpContextAccessor>();
            hca?.HttpContext?.Session.Remove(key);
        }


    }
}
