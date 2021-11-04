﻿using Autofac;
using System.Reflection;
using WebMVC.Service;

namespace WebApi6_0
{
    public class ConfigureAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IDependency); //IDependency 空的接口，所有接口继承它就可以了　　　　

            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "WebMVC.dll").Select(m => Assembly.LoadFrom(m)).ToArray();

            //自动注册接口
            builder.RegisterAssemblyTypes(assemblies).Where(type => baseType.IsAssignableFrom(type)).AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
 