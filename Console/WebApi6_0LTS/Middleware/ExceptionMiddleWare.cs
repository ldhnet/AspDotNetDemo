using Framework.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace WebApi6_0.Middleware
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
            using var memStream = new MemoryStream();
            var OriginalBody = context.Response.Body;

            try
            {
                WriteRequestLog(context);
                context.Response.Body = memStream;

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

                WriteResponseLog(context, memStream);
                await memStream.CopyToAsync(OriginalBody);
                context.Response.Body = OriginalBody;
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception != null)
            {
                logger.LogError("全局异常捕获：", exception.Message);
                var response = context.Response;
                var message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                response.ContentType = "application/json";
                await response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error(message, 400)));//.ConfigureAwait(false)
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
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("token已过期,请重新登录.", code)));
                        break;
                    case 403:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("未授权.", code)));
                        break;
                    default:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("未知错误", code)));
                        break;
                }
            }
        }

        private void WriteRequestLog(HttpContext context)
        {
            context.Request.EnableBuffering();
            var request = context.Request;
            request.EnableBuffering();
            var postJson = "";
            var stream = context.Request.Body;
            long? length = context.Request.ContentLength;
            if (length != null && length > 0)
            {
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                postJson = streamReader.ReadToEndAsync().Result;
            }
            context.Request.Body.Position = 0;
            string from = context.Request.HasFormContentType ? JsonConvert.SerializeObject(context.Request.Form) : string.Empty;

            //JsonConvert.SerializeObject(context.Request.Headers) -> Host
            logger.LogInformation(@$"Rquest detail
                    Headers:{context.Request.Headers.Host}
                    Querys:{JsonConvert.SerializeObject(context.Request.Query)}
                    Forms:{from}
                    Body:{postJson}");

        }

        private void WriteResponseLog(HttpContext context, MemoryStream memStream)
        {
            memStream.Position = 0;
            StreamReader streamReader = new StreamReader(memStream, Encoding.UTF8);
            string respJson = streamReader.ReadToEndAsync().Result;
            logger.LogInformation(@$"response
                    status code:{context.Response.StatusCode}
                    body:{respJson}");
            memStream.Position = 0;
        }

    }
}
