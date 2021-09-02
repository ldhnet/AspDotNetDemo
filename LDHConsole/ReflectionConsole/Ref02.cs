using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole
{
    public static class Ref02
    {
        public static void mainRef02()
        { 
            //1、反射基本的类 获取属性及方法
            Type type = typeof(Person);
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("类型名:" + type.Name);

            Console.WriteLine("类全名：" + type.FullName);

            Console.WriteLine("命名空间名:" + type.Namespace);

            Console.WriteLine("程序集名：" + type.Assembly);

            Console.WriteLine("模块名:" + type.Module);

            Console.WriteLine("基类名：" + type.BaseType);

            Console.WriteLine("是否类：" + type.IsClass);

            Console.WriteLine("类的公共成员(Public)：");

            MemberInfo[] memberInfos = type.GetMembers();//得到所有公共成员
            foreach (var item in memberInfos)
            {
                Console.WriteLine(string.Format("{0}:{1}", item.MemberType, item));
            }
            Console.WriteLine("类的公共属性(Public)：");
            PropertyInfo[] Propertys = type.GetProperties();
            foreach (PropertyInfo fi in Propertys)
            {
                Console.WriteLine(fi.Name);
            }

            Console.WriteLine("类的公共方法(Public)：");
            MethodInfo[] mis = type.GetMethods();
            foreach (MethodInfo mi in mis)
            {
                Console.WriteLine(mi.ReturnType + " " + mi.Name);
            }

            Console.WriteLine("类的公共字段(Public)：");
            FieldInfo[] fis = type.GetFields();
            foreach (FieldInfo fi in fis)
            {
                Console.WriteLine(fi.Name);
            }
        }
    }
}
