using Enyim.Caching;
using Enyim.Caching.Memcached;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private static Dictionary<int, string> _cacheList = new Dictionary<int, string>();
        public int UnitMethodTest(int a, int b)
        { 
            int c = a + b;
            return c;
        }

        public ActionResult Index()
        {
            //ConvertPdfPreview.Convert(@"D:\BY\LDHConsole\WebMVC\File\test01.docx", @"D:\BY\LDHConsole\WebMVC\File\test02.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

            //ConvertPdfPreview.Convert(@"D:\BY\LDHConsole\WebMVC\File\head.jpg", @"D:\BY\LDHConsole\WebMVC\File\head.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);

            SessionHelper.SetSession("testData", "ConvertPdfPreviewWdExportFormat");

            var aa = SessionHelper.GetSession("testData").ToString();

            // SessionHelper.Del("testData");

            // var bb = SessionHelper.GetSession("testData");

            //string  modules = "ConvertPdfPreviewWdExportFormat";
            //modules.ToCacheEx("testData");

            //var modulesw = CacheExtension.GetCache<string>("testData");


            Monitor.Enter(_cacheList);

            if (_cacheList.Keys.Contains(1) == false)
                _cacheList.Add(1, "test");

            Monitor.Exit(_cacheList);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}