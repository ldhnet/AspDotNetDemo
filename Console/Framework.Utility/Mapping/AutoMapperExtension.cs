using AutoMapper;
using AutoMapper.Configuration;
using Framework.Utility.Extensions;
using Framework.Utility.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Framework.Utility.Mapping
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        {
            service.TryAddSingleton<MapperConfigurationExpression>();
            service.TryAddSingleton<AutoInjectFactory>();

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
                var factory = serviceProvider.GetRequiredService<AutoInjectFactory>();

                foreach (var (sourceType, targetType) in factory.ConvertList)
                {
                    mapperConfigurationExpression.CreateMap(sourceType, targetType); 
                }

                var instance = new MapperConfiguration(mapperConfigurationExpression);

                instance.AssertConfigurationIsValid();

                return instance;
            });

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

                return mapperConfiguration.CreateMapper();
            });

            return service;
        }

        //public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        //{
        //    return applicationBuilder.ApplicationServices.GetRequiredService<MapperConfigurationExpression>();
        //}

        public static void UseMapperAutoInject(this IApplicationBuilder applicationBuilder, params Assembly[]? assemblys)
        {
            var factory = applicationBuilder.ApplicationServices.GetRequiredService<AutoInjectFactory>();

            if (assemblys == null || !assemblys.Any())
            {
                assemblys = Assembly.GetEntryAssembly()?
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .Where(c => c.FullName.Contains("DH.Models", StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            } 
            factory.AddAssemblys(assemblys);

            applicationBuilder.UseStateAutoMapper();
        } 
      
    }
}
