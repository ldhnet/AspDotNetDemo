using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.NLog;

namespace WebApi.Middleware
{
    /// <summary>
    /// 处理全局信息中间件
    /// </summary>
    public class ExceptionMiddleWare
    {
        private readonly ILogger<ExceptionMiddleWare> logger;
        /// <summary>
        /// 处理HTTP请求的函数。
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleWare(ILogger<ExceptionMiddleWare> _logger, RequestDelegate next)
        { 
            this.logger = _logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //抛给下一个中间件
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex);
            }
            finally
            {
                await WriteExceptionAsync(context, null);
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception != null)
            {
                logger.LogError("全局异常捕获：",exception);
                var response = context.Response;
                var message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                response.ContentType = "application/json";
                await response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error(message, 400))).ConfigureAwait(false);
            }
            else
            {
                var code = context.Response.StatusCode;
                logger.LogInformation("Response.StatusCode=" + code);
                switch (code)
                {
                    case 200:
                        return;
                    case 204:
                        return;
                    case 401:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("token已过期,请重新登录.", code))).ConfigureAwait(false);
                        break;
                    case 403:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("未授权.", code))).ConfigureAwait(false);
                        break;
                    default:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("未知错误", code))).ConfigureAwait(false);
                        break;
                }
            }
        }
    }
}
