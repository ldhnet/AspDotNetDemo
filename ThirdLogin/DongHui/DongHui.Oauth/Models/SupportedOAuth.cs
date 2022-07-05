namespace DongHui.Oauth.Models
{
    public class SupportedOAuth
    {
        public static Dictionary<string, string> dicList {
            get
            { 
                return new Dictionary<string, string>{
                    {"Baidu", "百度" },
                    {"Wechat", "微信公众号" },
                    {"Gitlab", "Gitlab" },
                    {"Gitee", "Gitee" },
                    {"Github", "Github" },
                    {"SinaWeibo", "新浪微博" },
                    {"Huawei", "华为" },
                    {"OSChina", "OSChina" },
                    {"QQ", "QQ" },
                    {"WechatOpen", "微信开放平台"},
                    {"DingTalk", "钉钉" },
                    {"Microsoft", "Microsoft" },
                    {"Mi", "小米" },
                };
            }
        }
    }
}
