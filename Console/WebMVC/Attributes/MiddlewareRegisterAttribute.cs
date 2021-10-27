using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebMVC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MiddlewareRegisterAttribute : Attribute
    {
        //注册顺序
        public int Sort { get; set; } = int.MaxValue;
        //生命周期
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
}
