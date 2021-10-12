using System;

namespace VirtualMethodTest
{
    class A
    {
        public virtual void Func() // 注意virtual,表明这是一个虚拟函数
        {
            Console.WriteLine("Func In A");
        }
    }

    class B : A // 注意B是从A类继承,所以A是父类,B是子类
    {
        public override void Func() // 注意override ,表明重新实现了虚函数
        {
            Console.WriteLine("Func In B");
        }
    }

    class C : B // 注意C是从A类继承,所以B是父类,C是子类
    { 
    }

    class D : A // 注意B是从A类继承,所以A是父类,D是子类
    {
        public new void Func() // 注意new ，表明覆盖父类里的同名类，而不是重新实现
        {
            Console.WriteLine("Func In D");
        }
    }

    class program
    {
        static void Main2()
        {
            A a;         // 定义一个a这个A类的对象.这个A就是a的申明类
            A b;         // 定义一个b这个A类的对象.这个A就是b的申明类
            A c;         // 定义一个c这个A类的对象.这个A就是b的申明类
            A d;         // 定义一个d这个A类的对象.这个A就是b的申明类

            a = new A(); // 实例化a对象,A是a的实例类
            b = new B(); // 实例化b对象,B是b的实例类
            c = new C(); // 实例化b对象,C是b的实例类
            d = new D(); // 实例化b对象,D是b的实例类

            a.Func();    // 执行a.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类A，就为本身 4.执行实例类A中的方法 5.输出结果 Func In A
            b.Func();    // 执行b.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类B，有重载的 4.执行实例类B中的方法 5.输出结果 Func In B
            c.Func();    // 执行c.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类C，无重载的 4.转去检查类C的父类B，有重载的 5.执行父类B中的Func方法 5.输出结果 Func In B
            d.Func();    // 执行d.Func：1.先检查申明类A 2.检查到是虚拟方法 3.转去检查实例类D，无重载的（这个地方要注意了，虽然D里有实现Func()，但没有使用override关键字，所以不会被认为是重载） 4.转去检查类D的父类A，就为本身 5.执行父类A中的Func方法 5.输出结果 Func In A
            D d1 = new D();
            d1.Func(); // 执行D类里的Func()，输出结果 Func In D
            Console.ReadLine();
        }
    }
}