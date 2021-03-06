using Microsoft.Extensions.DependencyInjection;

namespace Framework.Utility.Attributes
{
    public class MiddlewareRegisterInfo
    {
        public MiddlewareRegisterInfo(Type type, MiddlewareRegisterAttribute attribute)
        {
            Type = type;
            Sort = attribute.Sort;
            Lifetime = attribute.Lifetime;
        }
        public Type Type { get; private set; }
        public int Sort { get; private set; }
        public ServiceLifetime Lifetime { get; private set; }
    }
}
