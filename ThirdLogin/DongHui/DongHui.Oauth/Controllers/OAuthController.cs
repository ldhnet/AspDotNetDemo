using DongHui.Oauth.Extensions;
using DongHui.Oauth.Models;
using DongHui.OAuth.Gitee;
using Microsoft.AspNetCore.Mvc;

namespace DongHui.Oauth.Controllers
{
    public class OAuthController : Controller
    {
        private readonly ILogger<OAuthController> _logger;
        private readonly GiteeOAuth _giteeConfig;

        public OAuthController(ILogger<OAuthController> logger, GiteeOAuth giteeConfig)
        {
            _logger = logger;
            _giteeConfig = giteeConfig;
        }

        [HttpGet("oauth/{type}")]
        public IActionResult Index(string type)
        {
            var typeKey = type.Trim().ToLower();
            var info = SupportedOAuth.dicList.FirstOrDefault(c=>c.Key.ToLower() == typeKey);
            if (info.Key == null || info.Value is null)
            {
                return RedirectToAction("AccessDenied","Home");
            }
            return Redirect(_giteeConfig.GetAuthorizeUrl());
        }

        [HttpGet("oauth/{type}callback")]
        public async Task<IActionResult> LoginCallbackAsync(string type,
            [FromQuery] string code,
            [FromQuery] string state,
            [FromQuery] string error_description = "")
        {
            if (!string.IsNullOrEmpty(error_description))
            {
                throw new Exception(error_description);
            }
            if (code != null)
            {
                var access_token = await _giteeConfig.GetAccessTokenAsync(code, state);
                var userinfo = await _giteeConfig.GetUserInfoAsync(access_token);
                HttpContext.Session.Set("OAuthUser", userinfo);
            }
            return RedirectToAction("Privacy", "Home");
        }

       
    }
}