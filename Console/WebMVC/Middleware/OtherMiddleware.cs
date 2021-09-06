﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Middleware
{
    public class ResponseHeaderMiddleware
    {
        private readonly RequestDelegate next;
        public ResponseHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //这个例子只是修改一下response的header
            context.Response.OnStarting(state => {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("lang", "zh-CH");
                return Task.FromResult(0);
            }, context);
            await next(context);
        }

    }
}
