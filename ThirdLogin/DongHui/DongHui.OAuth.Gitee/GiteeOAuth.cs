using DongHui.OAuth.Core.Base;
using DongHui.OAuth.Core.Models;

namespace DongHui.OAuth.Gitee
{
    public class GiteeOAuth : OAuthLoginBase<GiteeAccessTokenModel, GiteeUserModel>
    {
        public GiteeOAuth(OAuthConfig oauthConfig) : base(oauthConfig) { }
        protected override string AuthorizeUrl => "https://gitee.com/oauth/authorize";
        protected override string AccessTokenUrl => "https://gitee.com/oauth/token";
        protected override string UserInfoUrl => "https://gitee.com/api/v5/user";
    }

}
