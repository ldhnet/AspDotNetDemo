using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.ContainerFactory
{
    public class PropertyInjectingContainerFactory : IServiceProviderFactory<PropertyInjectingContainerFactory.Builder>
    {
        public Builder CreateBuilder(IServiceCollection services)
        {
            return new Builder(services);
        }

        public IServiceProvider CreateServiceProvider(Builder containerBuilder)
        {
            return containerBuilder.CreateServiceProvider();
        }

        public class Builder
        {
            internal readonly IServiceCollection services;

            internal List<Type> attributeTypes = new List<Type>();

            public Builder(IServiceCollection services)
            {
                this.services = services;
            }

            public Builder AddInjectAttribute<A>() where A : Attribute
            {
                attributeTypes.Add(typeof(A));
                return this;
            }

            public IServiceProvider CreateServiceProvider() => new PropertyInjectingServiceProvider(services.BuildServiceProvider(), attributeTypes.ToArray());
        }

        class PropertyInjectingServiceProvider : IServiceProvider
        {
            private readonly IServiceProvider services;
            private readonly Type[] injectAttributes;

            public PropertyInjectingServiceProvider(IServiceProvider services, Type[] injectAttributes)
            {
                this.services = services;
                this.injectAttributes = injectAttributes;
            }

            // This function is only called for `IConfiguration` and `IHost` - why?
            public object GetService(Type serviceType)
            {
                var service = services.GetService(serviceType)!;
                InjectProperties(service);
                return service;
            }

            private void InjectProperties(Object target)
            {
                var type = target.GetType();

                var candidateProperties = type.GetProperties(System.Reflection.BindingFlags.Public);

                var props = from p in candidateProperties
                            where injectAttributes.Any(a => p.GetCustomAttributes(a, true).Any())
                            select p;

                foreach (var prop in props)
                {
                    prop.SetValue(target, services.GetService(prop.PropertyType));
                }
            }
        }
    }
}
