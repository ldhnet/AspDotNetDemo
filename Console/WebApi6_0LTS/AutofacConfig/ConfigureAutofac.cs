using Autofac;
using DH.Models.DbModels;
using Framework.Core.Data;
using Framework.Core.Dependency;
using System.Reflection;
using WebMVC.Service;

namespace WebApi6_0.AutofacConfig
{
    public class ConfigureAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IDependency); //IDependency 空的接口，所有接口继承它就可以了　　　　

            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "DirectService.*.dll,WebMVC.dll").Select(m => Assembly.LoadFrom(m)).ToArray();
             
            //自动注册接口
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf()   //自身服务，用于没有接口的类
                .AsImplementedInterfaces()  //接口服务
                .PropertiesAutowired()  //属性注入
                .SingleInstance();    //保证生命周期基于请求

            builder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}
