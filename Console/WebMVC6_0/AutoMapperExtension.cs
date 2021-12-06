using AutoMapper;
using AutoMapper.Configuration;
using Framework.Mapper; 
using Microsoft.Extensions.DependencyInjection.Extensions; 
namespace WebMVC6_0.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        {
            service.TryAddSingleton<MapperConfigurationExpression>(); 

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
                mapperConfigurationExpression.AddProfile(new MapperProfile());
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
         
      
    }
}
