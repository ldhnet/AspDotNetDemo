using DHLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.Attributes;
using WebMVC.Business;
using WebMVC.Common;
using WebMVC.Dto;
using WebMVC.Extension;
using WebMVC.Filter;
using WebMVC.Helper;
using WebMVC.Model;
using WebMVC.Models;

namespace WebMVC.Controllers
{ 
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private EmoloyeeDLL _userdll = new EmoloyeeDLL();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } 
        [HttpGet]
        [SkipLoginValidate]
        [ServiceFilter(typeof(MyFilter))]
        [AllowAnonymous]
        [ServiceFilter(typeof(ClientIpCheckActionFilter))]
        public IActionResult Index()
        {
            var aa = Path.DirectorySeparatorChar;
            var aa1 = Path.AltDirectorySeparatorChar;
            var aa2 = GlobalContext.HostingEnvironment.ContentRootPath;
            var aa3 = GlobalContext.HostingEnvironment.WebRootPath;
            var aa4 = GlobalContext.HostingEnvironment.WebRootFileProvider;
            var aa5 = GlobalContext.HostingEnvironment.ContentRootFileProvider;

            var aa6 = Environment.CurrentDirectory;

            var aa7 = Environment.CurrentManagedThreadId;

            SetCookies("cookieKay", "11111条数据的内容1111");
             
            var contents = new List<Content>();
            for (int i = 0; i < 50; i++)
            {
                contents.Add(new Content { Id = i, Title = $"第{i}条数据标题测试", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now.AddDays(-i) });
            }
            SessionHelper.SetSession("sessionKey", "内容22222222222222");


            var cc=  SessionHelper.GetSession("sessionKey");

            var bb = GetCookies("cookieKay");
              
            var UserCacheModel = HttpContext.Session.GetObjectFromJson<UserCacheModel>(WebConstant.SessionKey.UserCacheModel);

            HttpContext.Session.SetString("param", "测试测试sessionKey");
            var value = HttpContext.Session.GetString("param");

             
            RetryPolicyHelper.Retry(() => _logger.LogError("测试内容22222222222222"));


            return View(new ContentViewModel { Contents = contents }); 
        }
 
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            //throw new Exception("异常了");
            //var dto = new UserViewModel();

             
            var employee = _userdll.Find("admin")?.Data;

            var dto = new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                BankCardDisplay = employee.BankCardDisplay,
                MoneryDisplay = employee.MoneryDisplay,
                other = employee.MoneryDisplay
            };

            //var dto = new EmployeeDto { Id = 12345, Name = "用户12345" };

            return View(dto);
        }

        [HttpGet]
        [AllowAnonymous]
        public EmployeeDto Get([ModelBinder(typeof(HashIdModelBinder))] EmployeeDto dto)
        {
            return new EmployeeDto { Id = dto.Id, Name = "用户" + dto.Id };
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult NoPermission()
        {
            return View();
        }


        [HttpPost]
        [PreventDoublePost]
        public IActionResult Edit(Employee model)
        {
            if (!ModelState.IsValid)
            {
                //PreventDoublePost Attribute makes ModelState invalid
            }
            throw new NotImplementedException();
        }

    }
}
