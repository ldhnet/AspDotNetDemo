using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcClient_2.Models; 
using System.Security.Claims;

namespace MvcClient_2.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        //[Route("Login")]
        public IActionResult Index(string ReturnUrl = "")
        { 
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(UserLogin model)
        {
            var claims = new[] {
                    new Claim(ClaimTypes.Email, "574427343@qq.com"),
                    new Claim(ClaimTypes.SerialNumber,model.Account),
                    new Claim(ClaimTypes.Name,model.Account),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity); 
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                //设置cookie票证的过期时间
                ExpiresUtc = DateTime.Now.AddDays(1),
                RedirectUri = model.ReturnUrl
            }; 
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, properties);

            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return LocalRedirect("/");
            }
            return LocalRedirect(model.ReturnUrl);
        }
        [HttpGet]
        public ActionResult Logout()
        { 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
        }
    }
}
