using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace WebMVC.Controllers
{
    public class LangController : Controller
    {
        //private readonly IStringLocalizer _lang;

        public IActionResult Index()
        {
            var currentUICulture = CultureInfo.CurrentUICulture.Name;
            var currentCulture = CultureInfo.CurrentCulture.Name;

            ViewBag.Culture = currentCulture;

            ViewBag.UICulture = currentUICulture;

            ViewBag.Lang = "en";
            return View();
        }
    }
}
