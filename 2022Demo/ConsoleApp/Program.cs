using ConsoleApp;
using ConsoleApp._0331.LockUt;
using ConsoleApp.多线程;
using ConsoleApp.多线程锁;
using System.Diagnostics;

//LockTest lockTest = new LockTest();

//lockTest.qiangdan();

//MonitorTest monitorTest = new MonitorTest();

//monitorTest.qiangdan();

//RedisLockTest redisTest = new RedisLockTest();

//redisTest.qiangdan();


//ThreadTestA.GetIncrement();

//AtomicityForLockTest.AtomicityForLock();

//AtomicityForCASTest.AtomicityForInterLock();

//AtomicityForCASTest.AtomicityForMyCalc();


//SpinklockTest.Spinklock();

//TaskDemoA.TaskMain();

///TaskDemoB.TaskMain();

//TaskDemoC taskDemoC = new TaskDemoC();
//taskDemoC.GetStringAsync();

TaskDemo taskDemo=new TaskDemo();
await taskDemo.GetTaskRun();

//taskDemo.CancelTask();

Console.WriteLine("Hello, World!");