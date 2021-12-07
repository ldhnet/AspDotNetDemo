using Microsoft.AspNetCore.Builder;
using System;
using WebApi6_0.Middleware;

namespace WebApi6_0.Middleware
{
    public static class CalculateExecutionTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseCalculateExecutionTime(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<CalculateExecutionTimeMiddleware>();
        }
    }
}
