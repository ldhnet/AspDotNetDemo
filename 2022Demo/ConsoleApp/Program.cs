using ConsoleApp;
using ConsoleApp._0331.LockUt;
using ConsoleApp._0605;
using ConsoleApp.Cache;
using ConsoleApp.多线程;
using ConsoleApp.多线程锁;
using System.Diagnostics;
using ConsoleApp._0616;
using ConsoleApp.Adapter;
using WebA.Business;

var test3 = new yield_3_test();
test3.Init();

var test2=new yield_2_Test();

test2.testMain();

Console.WriteLine("Hello, World!");

//ChannelTest test=new ChannelTest();
//test.testMain();

//Console.WriteLine("Hello, World!");

//Test test=new Test();

//test.责任链工厂Start();


//Console.WriteLine("Hello, World!");

//AdapterTests.TestMain();

//ClosureTests.TestMain();

//yieldTest.testMain();



//MemoryCacheB.MemoryCacheTest();

//SqlConnectionTest sqlConnectionTest =new SqlConnectionTest();
//sqlConnectionTest.tes

//SqlConnectionTest.TestSQLServerConnectionCount();


//RemoteService remoteService = new RemoteService();
//remoteService.startService();

//Console.WriteLine("Hello, World!");

//ScheduleTask scheduleTask = new ScheduleTask();
//scheduleTask.ScheduleTaskMain();





//MemoryCacheA.MemoryCacheTest();
//MemoryCacheB.MemoryCacheTest();

//Console.WriteLine("Hello, World!");



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

//TaskDemo taskDemo=new TaskDemo();
//await taskDemo.GetTaskRun();

//taskDemo.CancelTask();



//int y = 10;

////Func<int, int> sum = x =>
////{
////    return x + y;
////};

////面向对象改造
//FuncClass funcClass = new FuncClass() { y = y };

//Func<int, int> sum = funcClass.Run;

//Console.WriteLine(sum(11));

