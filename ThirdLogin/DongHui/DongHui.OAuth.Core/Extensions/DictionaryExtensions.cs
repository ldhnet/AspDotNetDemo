using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DongHui.OAuth.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this Dictionary<string, string> dict, bool urlEncode = true)
        {
            return string.Join("&", dict.Select((KeyValuePair<string, string> p) => ((!urlEncode) ? "" : p.Key?.UrlEncode()) + "=" + ((!urlEncode) ? "" : p.Value?.UrlEncode())));
        }

        public static string UrlEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            return HttpUtility.UrlEncode(str, Encoding.UTF8);
        }

        public static void RemoveEmptyValueItems(this Dictionary<string, string> dict)
        {
            (from item in dict
             where string.IsNullOrEmpty(item.Value)
             select item.Key).ToList().ForEach(delegate (string key)
             {
                 dict.Remove(key);
             });
        }
    }
}
