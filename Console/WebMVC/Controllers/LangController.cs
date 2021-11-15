using DH.Models.DbModels;
using Framework.Utility;
using LangResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading; 

namespace WebMVC.Controllers
{
    public class LangController : BaseController
    { 
        private readonly IStringLocalizer<LangResource> _localizer;
   
        public LangController(IStringLocalizer<LangResource> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var aaaa = _localizer.GetString(LangResource.Hello);
            var bbb = _localizer.GetString(LangResource.Title);
            var aaaa1 = _localizer.GetString(LangResource.AnyRadixConvert_CharacterIsNotValid);

            var ccc = _localizer.GetString("CheckNotNull");


            var ddd = string.Format(ccc,nameof(ccc));

            var aa = new Employee();

            var msg = "";
            try
            {
                Check.NotNull(aa.BankCard, nameof(aa));
            }
            catch (System.Exception ex)
            {

                msg = ex.Message;
            }
          
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

            ViewBag.Lang =  aaaa + "=" + bbb + ddd + msg + aaaa1;

            return View();
        }
    }
}
