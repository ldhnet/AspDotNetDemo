using DongHui.OAuth.Core.Extensions;
using DongHui.OAuth.Core.Helper;
using DongHui.OAuth.Core.Interfaces;
using DongHui.OAuth.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DongHui.OAuth.Core.Base
{

    public abstract class OAuthLoginBase<TAccessTokenModel, TUserInfoModel> where TAccessTokenModel : class, IAccessTokenModel, new() where TUserInfoModel : IUserInfoModel
    {
        protected readonly OAuthConfig oauthConfig;

        protected abstract string AuthorizeUrl { get; }

        protected abstract string AccessTokenUrl { get; }

        protected abstract string UserInfoUrl { get; }

        public OAuthLoginBase(OAuthConfig oauthConfig)
        {
            this.oauthConfig = oauthConfig;
        }
        protected virtual Dictionary<string, string> BuildAuthorizeParams(string state)
        {
            return new Dictionary<string, string>
            {
                ["response_type"] = "code",
                ["client_id"] = (oauthConfig.AppId ?? ""),
                ["redirect_uri"] = (oauthConfig.RedirectUri ?? ""),
                ["scope"] = (oauthConfig.Scope ?? ""),
                ["state"] = (state ?? "")
            };
        }
        protected virtual Dictionary<string, string> BuildGetAccessTokenParams(Dictionary<string, string> authorizeCallbackParams)
        {
            return new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["code"] = (authorizeCallbackParams["code"] ?? ""),
                ["client_id"] = (oauthConfig.AppId ?? ""),
                ["client_secret"] = (oauthConfig.AppKey ?? ""),
                ["redirect_uri"] = (oauthConfig.RedirectUri ?? "")
            };
        }

        protected virtual Dictionary<string, string> BuildGetUserInfoParams(TAccessTokenModel accessTokenModel)
        {
            return new Dictionary<string, string> { ["access_token"] = accessTokenModel.AccessToken };
        }


        public virtual string GetAuthorizeUrl(string state = "")
        {
            Dictionary<string, string> dict = BuildAuthorizeParams(state);
            dict.RemoveEmptyValueItems();
            return AuthorizeUrl + (AuthorizeUrl.Contains("?") ? "&" : "?") + dict.ToQueryString();
        }

        public virtual Task<TAccessTokenModel> GetAccessTokenAsync(string code, string state = "")
        {
            return GetAccessTokenAsync(new Dictionary<string, string>
            {
                ["code"] = code,
                ["state"] = state
            });
        }

        public virtual async Task<TAccessTokenModel> GetAccessTokenAsync(Dictionary<string, string> authorizeCallbackParams)
        {
            var val = await HttpRequestApi.PostAsync<TAccessTokenModel>(AccessTokenUrl, BuildGetAccessTokenParams(authorizeCallbackParams));
            VerifyAccessTokenModel(val);
            return val;
        }
        public virtual Task<TUserInfoModel> GetUserInfoAsync(string accessToken)
        {
            return GetUserInfoAsync(new TAccessTokenModel
            {
                AccessToken = accessToken
            });
        }

        public virtual async Task<TUserInfoModel> GetUserInfoAsync(TAccessTokenModel accessTokenModel)
        {
            TUserInfoModel val = JsonSerializer.Deserialize<TUserInfoModel>(await HttpRequestApi.GetStringAsync(UserInfoUrl, BuildGetUserInfoParams(accessTokenModel)), (JsonSerializerOptions)null);
            VerifyUserInfoModel(val);
            return val;
        }


        protected virtual void VerifyAccessTokenModel(TAccessTokenModel accessTokenModel)
        {
            if (accessTokenModel.HasError())
            {
                throw new Exception(accessTokenModel.ErrorDescription);
            }
        }
        protected virtual void VerifyUserInfoModel(TUserInfoModel userInfoModel)
        {
            if (userInfoModel.HasError())
            {
                throw new Exception(userInfoModel.ErrorMessage);
            }
        }

    }

}
