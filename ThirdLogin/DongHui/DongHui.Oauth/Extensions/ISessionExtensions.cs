using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace DongHui.Oauth.Extensions
{
    public static class ISessionExtensions
    {
        private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        public static void Set(this ISession Session, string key, object data, bool jsonIndentFormat = false)
        {
            if (data == null)
            {
                return;
            }
            var json = "";
            if (jsonIndentFormat)
            {
                json = JsonSerializer.Serialize(data, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                });
            }
            else
            {
                json = JsonSerializer.Serialize(data, JsonSerializerOptions);
            }
            Session.SetString(key, json);
        }

        public static T Get<T>(this ISession Session, string key)
        {
            return JsonSerializer.Deserialize<T>(Session.GetString(key), JsonSerializerOptions);
        }
    }
}
