using System;
using System.Collections.Generic;
/// <summary>
/// 适用于在多线程的情况下保证只有一个实例化对象的情况，例如银行的操作系统
/// </summary>
namespace ConsoleApp.Gof
{
    //----------------------------------
    // 双重锁定单例
    public  class Singleton
    {
        // 定义一个类对象，用于内部实现
        private static Singleton myInstance;

        // readonly   -   这个成员只能在“类初始化”时赋值  ,所谓的类初始化，就是直接在类里面初始化
        // 变量标记为 readonly，第一次引用类的成员时创建实例
        private static readonly object lockRoot = new object();

        // 设置构造方法为私有，这样就不能在外部实例化类对象了
        private Singleton()
        {
        }

        // 实例化对象的方法
        public static Singleton GetInstance()
        {
            // 外部不能实例化对象，但是能调用类里面的静态方法
            // 外部需要调用这个方法来使用类对象，如果对象不存在就创建
            // 这里面使用两个判断是否为null的原因是，我们不需要每次都对实例化的语句进行加锁，只有当对象不存在的时候加锁就可以了
            if (myInstance == null)
            {
                // 锁定的作用就是为了保证当多线程同时执行这句代码的时候保证对象的唯一性
                // 锁定会让同时执行这段代码的线程排队执行
                // lock里面需要用一个已经存在的对象来判断，所以不能使用myInstance
                lock (lockRoot)
                {
                    // 这里还需要一个判断的原因是，如果多线程都通过了外层的判断进行排队
                    // 那将会实例化多个对象出来，所以这里还需要进行一次判断，保证线程的安全
                    if (myInstance == null)
                    {
                        myInstance = new Singleton();
                    }
                }
            }
            return myInstance;
        }
    }
}