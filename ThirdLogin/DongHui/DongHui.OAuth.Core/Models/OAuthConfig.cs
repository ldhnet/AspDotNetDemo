using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Models
{
    public class OAuthConfig
    {
        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string RedirectUri { get; set; }
        public string Scope { get; set; }

        public static OAuthConfig LoadFrom(IConfiguration configuration, string prefix)
        {
            return With(configuration[prefix + ":app_id"], configuration[prefix + ":app_key"], configuration[prefix + ":redirect_uri"], configuration[prefix + ":scope"]);
        }

        public static OAuthConfig With(string appId, string appKey, string redirectUri, string scope)
        {
            return new OAuthConfig
            {
                AppId = appId,
                AppKey = appKey,
                RedirectUri = redirectUri,
                Scope = scope
            };
        }
    }
}
