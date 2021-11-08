using LangResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading;
using WebMVC.Properties;

namespace WebMVC.Controllers
{
    public class LangController : BaseController
    { 
        private readonly IStringLocalizer<LangResource> localizer;

        public LangController(IStringLocalizer<LangResource> _localizer)
        {
            localizer= _localizer;
        }
        public IActionResult Index()
        {

            var aaaa = localizer.GetString(LangResource.Hello);
            var bbb = localizer.GetString(LangResource.Title);

            //var langRequest = Request.Headers["accept-language"].ToString();

            //Request.Cookies.TryGetValue("lang", out string lang); 

            //CultureInfo cinfo = new CultureInfo(lang ?? "zh-CN");
            //Thread.CurrentThread.CurrentCulture = cinfo;
            //Thread.CurrentThread.CurrentUICulture = cinfo;

            //var currentUICulture = CultureInfo.CurrentUICulture.Name;
            //var currentCulture = CultureInfo.CurrentCulture.Name;

            //ViewBag.Culture = currentCulture;
            //ViewBag.UICulture = currentUICulture;


            //ViewBag.Lang = Resources.Title + "---" + Resources.Hello + "====" + aaaa + bbb;

            ViewBag.Lang =  aaaa + "====" + bbb;

            return View();
        }
    }
}
