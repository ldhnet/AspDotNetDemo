using Autofac; 
using Framework.Core.Data;
using Framework.Core.Dependency;
using Framework.EF.Context;
using System.Reflection;

namespace WebApi6_0.AutofacConfig
{
    public class ConfigureAutofac : Autofac.Module
    {
        //protected override void Load(ContainerBuilder builder)
        //{
        //    var baseType = typeof(IDependency); //IDependency 空的接口，所有接口继承它就可以了　　　　

        //    Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "DirectService.*.dll,WebMVC.dll").Select(m => Assembly.LoadFrom(m)).ToArray();

        //    //自动注册接口
        //    builder.RegisterAssemblyTypes(assemblies)
        //        .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
        //        .AsSelf()   //自身服务，用于没有接口的类
        //        .AsImplementedInterfaces()  //接口服务
        //        .PropertiesAutowired()  //属性注入
        //        .SingleInstance();    //保证生命周期基于请求

        //    builder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
        //}

        protected override void Load(ContainerBuilder builder)
        {
            Type baseType = typeof(IDependency);
            var assemblies = Assembly.GetEntryAssembly()?//获取默认程序集
                    .GetReferencedAssemblies()//获取所有引用程序集
                    .Select(Assembly.Load)
                    .Where(c => c.FullName.Contains("DirectService", StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(baseType) && !type.IsAbstract)
                .AsSelf()   //自身服务，用于没有接口的类
                .AsImplementedInterfaces()  //接口服务
                .PropertiesAutowired()  //属性注入
                .InstancePerLifetimeScope();    //保证生命周期基于请求  

            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            builder.RegisterType<MyDBContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

    }
}
