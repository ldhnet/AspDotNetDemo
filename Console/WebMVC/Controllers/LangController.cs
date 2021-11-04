using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading;
using WebMVC.Properties;

namespace WebMVC.Controllers
{
    public class LangController : BaseController
    { 
        private readonly IStringLocalizer<Resources> _localizer;

        public LangController(IStringLocalizer<Resources> localizer)
        {
            _localizer= localizer;
        }
        public IActionResult Index()
        {

            var langRequest = Request.Headers["accept-language"].ToString();

            Request.Cookies.TryGetValue("lang", out string lang); 

            CultureInfo cinfo = new CultureInfo(lang ?? "zh-CN");
            Thread.CurrentThread.CurrentCulture = cinfo;
            Thread.CurrentThread.CurrentUICulture = cinfo;
             
            var currentUICulture = CultureInfo.CurrentUICulture.Name;
            var currentCulture = CultureInfo.CurrentCulture.Name;

            ViewBag.Culture = currentCulture;
            ViewBag.UICulture = currentUICulture;
             

            ViewBag.Lang = Resources.Title;


            return View();
        }
    }
}
