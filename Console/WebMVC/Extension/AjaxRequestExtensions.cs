using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Utility.Extensions
{
    public static class AjaxRequestExtensions
    {
        public static bool IsAjax(this HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
        //public static bool IsAjaxRequest(this HttpRequest request)
        //{
        //    if (request == null)
        //        throw new ArgumentNullException("request");

        //    if (request.Headers != null)
        //        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        //    return false;
        //}
    }
}
