using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMVC.Entities;
using WebMVC.Models;

namespace WebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var aaaa3 = ConnectionStringManager.CustomerSection;

            var aaaa = ConnectionStringManager.PinkCarsConnectionString;
            var aaaa1 = ConnectionStringManager.DataMartConnectionString;
            var aaaa2 = ConnectionStringManager.HangfireConnectionString;
            var aaaa4 = ConnectionStringManager.ConsultantsConnectionString;


            var aa = ConfigurationManager.AppSettings;

            var aa1 = ConfigurationManager.AppSettings.Get("DBConnectionString");

     

            var bb = ConfigurationManager.ConnectionStrings;
             

            var cc = ConfigurationManager.ConnectionStrings[aa1];
            var cc1 = ConfigurationManager.ConnectionStrings[aa1].ProviderName;

            var bb1 = ConfigurationManager.ConnectionStrings.CurrentConfiguration;

            var bb2 = ConfigurationManager.GetSection("enyim.com/memcached");


            var bb5 = ConfigurationManager.GetSection("SysOptionGroup/SysOptionItems");

            var bb3 = (IDictionary)ConfigurationManager.GetSection("SysOptionGroup/SysOptionItems");
             

            BackgroundWorker();
        }
        private  void BackgroundWorker()
        {
            var worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += (sender, e) =>
            {
                int sum = 0;
                for (int i = 0; i <= 100; i++)
                {
                    sum += i;

                    System.Diagnostics.Debug.WriteLine($"-----单元测试 {i} -----");
                }
                System.Threading.Thread.Sleep(3000);
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                worker.RunWorkerAsync();
            };

            worker.RunWorkerAsync();

        }
    }
}
