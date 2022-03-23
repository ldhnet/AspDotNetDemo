using Framework.Utility;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;

namespace WebApi6_0.Middleware
{
    public class DebugMiddleware
    {
        private readonly IFeatureManager _featureManager;
        private readonly RequestDelegate _next;

        public DebugMiddleware(RequestDelegate next, IFeatureManager featureManager)
        {
            _next = next;
            _featureManager = featureManager;
        } 
        public async Task InvokeAsync(HttpContext context)
        {
            var isDebugEndpoint = context.Request!.Path.Value!.ToLower().Contains("test");//test
            var debugEndpoint = await _featureManager.IsEnabledAsync("ForbiddenDebugEndpoint");

            if (isDebugEndpoint && debugEndpoint)
            {
                context.SetEndpoint(new Endpoint((context) =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;


                    context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Error("无权访问", StatusCodes.Status403Forbidden)));
                     
                    return Task.CompletedTask;
                },
                EndpointMetadataCollection.Empty,  "无权访问"));
            }
            await _next.Invoke(context);

        }
         
    }
}
