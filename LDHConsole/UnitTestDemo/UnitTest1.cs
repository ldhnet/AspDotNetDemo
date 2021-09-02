using ClassLibrary4._8;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;
using WebMVC.Controllers;


namespace UnitTestDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod()
        {
            System.Diagnostics.Debug.WriteLine("-----单元测试 方法TestMethod 开始-----");

            var aa = AppDomain.CurrentDomain.BaseDirectory;

            Xmltest xmlOpt = new Xmltest();
            xmlOpt.XmlSave();
            xmlOpt.XmlUpdate();

            xmlOpt.GetXmlDocument();
 
           
            xmlOpt.GetXmlTextReader();
    
      
            //XmlSave();

            System.Diagnostics.Debug.WriteLine("-----单元测试 方法TestMethod 结束-----");

        }
        public void XmlSave()
        {
            var doc = new XDocument(
              new XElement("Contacts",
                    new XElement("Contact",
                            new XAttribute("id", "01"),
                    new XElement("Name", "Daisy Abbey"),
                            new XElement("Gender", "female")
                                )
                            ) 

              );
            doc.Save("test2.xml");
        }
        [TestMethod]
        public void TestMethod1()
        {
            System.Diagnostics.Debug.WriteLine("-----单元测试 方法TestMethod1 开始-----");
            HomeController homeController = new HomeController();
            int c = homeController.UnitMethodTest(6, 3);

            test1();


            System.Diagnostics.Debug.WriteLine("测试结果：" + c);
            System.Diagnostics.Debug.WriteLine("-----单元测试 方法TestMethod1 结束-----");

        }
        [TestMethod]
        public void TestMethod2()
        {
            UnitDHLibrary _unit = new UnitDHLibrary();
          
            var result = _unit.addTest(2,3);
            System.Diagnostics.Debug.WriteLine("测试结果：" + result);
            System.Diagnostics.Debug.WriteLine("-----单元测试 方法TestMethod2 结束-----");
        }

        [TestMethod]
        public void GetTriangle_Test()
        { 
            string[] sideArr = { "5", "5", "5" };
            var result = UnitDHLibrary.GetTriangle(sideArr);
            System.Diagnostics.Debug.WriteLine("测试结果：" + result);
            Assert.AreEqual("等边三角形", UnitDHLibrary.GetTriangle(sideArr));
            System.Diagnostics.Debug.WriteLine("-----单元测试 方法GetTriangle_Test 结束-----");
        }
        private static void test1()
        {
            int processMonth = ToIntyyyyMM(DateTime.Now);
            int currentMonth = processMonth % 100;
            var sss = Math.Floor(Convert.ToDouble(processMonth / 100));
            var startDate = new DateTime(Convert.ToInt32(Math.Floor(Convert.ToDouble(processMonth / 100))), currentMonth, 1)
                                        .AddMonths(3 * -1);


            var endT = DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(-3).Date; 
        }
        private static int ToIntyyyyMM(DateTime currentProcessMonth)
        {
            return Convert.ToInt32(currentProcessMonth.ToString("yyyyMM"));
        }
        private static int Factorial(int n)
        {
            int sum = 0;
            if (0 == n)
                return 1;
            else
                sum = n * Factorial(n-1);
            return sum;
        }

    }

}
