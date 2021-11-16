using DH.Models.DbModels;
using Framework.Utility;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class LangController : BaseController
    {
        public IActionResult Index()
        {
            var aaaa = ShardResource.Hello;
            var bbb = ShardResource.Title;
            var ccc = ShardResource.CheckNotNull;

            var ddd = string.Format(ccc, nameof(ccc));

            var aa = new Employee();

            var msg = "";
            try
            {
                Check.NotNull(aa.BankCard, nameof(aa));
            }
            catch (System.ArgumentNullException ex)
            {
                msg = ex.ParamName;
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

            ViewBag.Lang = aaaa + "=" + bbb + "<br> \n" + ddd + "<br> \n" + msg;

            return View();
        }
    }
}
