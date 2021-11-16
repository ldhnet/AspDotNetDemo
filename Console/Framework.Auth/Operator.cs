using Framework.Utility.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web; 

namespace Framework.Auth
{
    public class Operator
    {
        public static Operator Instance
        {
            get { return new Operator(); }
        }

        private string LoginProvider = "WebApi";
        private string TokenName = "UserToken"; //cookie name or session name

        public void AddCurrent(string token)
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    //new CookieHelper().WriteCookie(TokenName, token);
                    break;

                case "Session":
                    //new SessionHelper().WriteSession(TokenName, token);
                    break;

                case "WebApi":
                    OperatorInfo user = new DataRepository().GetUserByToken(token);
                    if (user != null)
                    {
                        //CacheFactory.Cache.SetCache(token, user);
                    }
                    break;

                default:
                    throw new Exception("未找到LoginProvider配置");
            }
        }

        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        public void RemoveCurrent(string apiToken = "")
        {
            switch (LoginProvider)
            {
                case "Cookie":
                    //new CookieHelper().RemoveCookie(TokenName);
                    break;

                case "Session":
                    //new SessionHelper().RemoveSession(TokenName);
                    break;

                case "WebApi":
                    //CacheFactory.Cache.RemoveCache(apiToken);
                    break;

                default:
                    throw new Exception("未找到LoginProvider配置");
            }
        }

        /// <summary>
        /// Api接口需要传入apiToken
        /// </summary>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        public OperatorInfo? Current(string apiToken = "")
        {
            IHttpContextAccessor? hca = GlobalConfig.ServiceProvider?.GetService<IHttpContextAccessor>();
            OperatorInfo? user = null;
            string token = string.Empty;
            switch (LoginProvider)
            {
                case "Cookie":
                    if (hca.HttpContext != null)
                    {
                        //token = new CookieHelper().GetCookie(TokenName);
                    }
                    break;

                case "Session":
                    if (hca.HttpContext != null)
                    {
                        //token = new SessionHelper().GetSession(TokenName);
                    }
                    break;

                case "WebApi":
                    token = apiToken;
                    break;
            }
            if (string.IsNullOrEmpty(token))
            {
                return user;
            }
            token = token.Trim('"');

            user = new DataRepository().GetUserByToken(token);


            //user = CacheFactory.Cache.GetCache<OperatorInfo>(token);
            //if (user == null)
            //{
            //    user = new DataRepository().GetUserByToken(token);
            //    if (user != null)
            //    {
            //        CacheFactory.Cache.SetCache(token, user);
            //    }
            //}
            return user;
        }
    }
}
