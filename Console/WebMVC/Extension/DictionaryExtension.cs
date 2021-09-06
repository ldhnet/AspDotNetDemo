using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Extension
{
    public static class DictionaryExtension
    {
       
        /// <summary>
        /// fetch value from dict by specified key, otherwise return default value
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V Get<K, V>(this Dictionary<K, V> dict, K key)
        {
            if (dict.ContainsKey(key)) return dict[key];
            return default(V);
        }

        /// <summary>
        /// if couldn't find from dict, fetch from full dict, otherwise return default value
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="fullDict"></param>
        /// <returns></returns>
        public static V Get<K, V>(this Dictionary<K, V> dict, K key, Dictionary<K, V> fullDict)
        {
            if (dict.ContainsKey(key)) return dict[key];
            else if (fullDict.ContainsKey(key)) return fullDict[key];
            return default(V);
        }
    }
}
