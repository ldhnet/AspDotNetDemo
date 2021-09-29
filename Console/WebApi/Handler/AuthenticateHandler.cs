using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.OAuth;

namespace WebApi.Handler
{
    public class AuthenticateHandler : IAuthenticationHandler
    {
        public const string SchemeName = "MyAuth";

        AuthenticationScheme _scheme;
        HttpContext _context;

        /// <summary>
        /// 初始化认证
        /// </summary>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 认证处理
        /// </summary>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var req = _context.Request.Query;


      
             
            var isLogin = req["isLogin"].FirstOrDefault();

            if (isLogin != "true")
            {
                //return Task.FromResult(AuthenticateResult.Fail("未登陆"));
            }
            var clientAuth = MappingAccessTokenAnalyzer.GetAccessToken("");
            var ticket = GetAuthTicket("test", "test");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        AuthenticationTicket GetAuthTicket(string name, string role)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),

                new Claim(ClaimTypes.DateOfBirth, role),
                new Claim(ClaimTypes.Gender, role),
                new Claim(ClaimTypes.SerialNumber, role),
                new Claim(ClaimTypes.Email, role),
                new Claim(ClaimTypes.MobilePhone, role),
                new Claim(ClaimTypes.OtherPhone, role),
                new Claim(ClaimTypes.Country, role),
                new Claim(ClaimTypes.AuthenticationMethod, role),
                new Claim(ClaimTypes.Authentication, role),
                new Claim(ClaimTypes.Anonymous, role),
                new Claim(ClaimTypes.HomePhone, role),
            }, "My_Auth");

            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }

        /// <summary>
        /// 权限不足时的处理
        /// </summary>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 未登录时的处理
        /// </summary>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }
    }
}
