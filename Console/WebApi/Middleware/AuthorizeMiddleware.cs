using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.OAuth;

namespace WebApi.Middleware
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AuthorizeMiddleware> logger;
        public AuthorizeMiddleware(RequestDelegate next, ILogger<AuthorizeMiddleware> _logger)
        {
            this.next = next;
            this.logger = _logger;
        }
        public async Task Invoke(HttpContext context) /* other scoped dependencies */
        {
            //token类型是bearer类型
            if (context.Request.Headers.ContainsKey("NoAuth") && context.Request.Headers["NoAuth"].ToString().ToLower() == "yes")
            {
                logger.LogInformation("WebApi:已跳过 Authorize 身份验证中间件");
                await next(context);
            }
            logger.LogInformation("WebApi:Authorize 身份验证中间件");
            //以下代码都不是必须的，只是展示一些使用方法，你可以选择使用

            //设置httpcontext的一些key和value，用于在整个http请求过程中分享数据
            context.Items.Add("aaa", "bbb");

            //判断当前http请求的url是否处理
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                //dosomething

            }

            //token类型是bearer类型
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"];

                string _token = token.ToString().Substring(7);


                var clientAuth = MappingAccessTokenAnalyzer.GetAccessToken(_token);

                if (_token != clientAuth.Token)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
                if (clientAuth.ExpirationDateUtc < DateTime.Now)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }
            else
            {

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
            //获取cookie
            var cookie = context.Request.Cookies["BAIDUD"];
            if (cookie == null)
            {
                //dosomething
            }

            //从http的url参数获取一个值
            var value1 = context.Request.Query.FirstOrDefault(u => u.Key == "accesstoken");
            if (value1.Value.Count <= 0)
            {
                //dosomething
            }

            //这个例子只是修改一下response的header
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("test2", "testvalue2");
                return Task.FromResult(0);
            }, context);
            //处理结束转其它中间组件去处理
            await next(context);
        }

    }
}
