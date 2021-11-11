using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Utility;
using Framework.Utility.Attributes;
using Framework.Utility.Helper;
using HashidsNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using WebMVC.Attributes; 
using WebMVC.Common;
using WebMVC.Dto;
using WebMVC.Extension;
using WebMVC.Filter;
using WebMVC.Helper;
using WebMVC.Model;
using WebMVC.Models;
using static WebMVC.Helper.MemoryCacheHelper;

namespace WebMVC.Controllers
{ 
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache; 
        public HomeController(ILogger<HomeController> logger, IUserService userService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _userService = userService;
            _memoryCache = memoryCache;
        } 
        [HttpGet]
        [SkipLoginValidate]
        [ServiceFilter(typeof(MyFilter))]
        [AllowAnonymous]
        [ServiceFilter(typeof(ClientIpCheckActionFilter))]
        public IActionResult Index()
        {  
            _logger.LogError("Index 11111");
            _logger.LogInformation("Index 22222");
            _logger.LogDebug("Index 33333");
            _logger.LogTrace("Index 44444");
            _logger.LogWarning("Index 55555");

            var aaa =  HttpUtility.UrlEncode("https://www.cnblogs.com/dongh/p/15470938.html");

            var bbb = HttpUtility.UrlDecode("https://www.cnblogs.com/dongh/p/15470938.html");

            _logger.LogInformation(aaa);
            _logger.LogInformation(bbb);

            var aa = Path.DirectorySeparatorChar;
            var aa1 = Path.AltDirectorySeparatorChar;
            var aa2 = GlobalContext.HostingEnvironment.ContentRootPath;
            var aa3 = GlobalContext.HostingEnvironment.WebRootPath;
            var aa4 = GlobalContext.HostingEnvironment.WebRootFileProvider;
            var aa5 = GlobalContext.HostingEnvironment.ContentRootFileProvider;

            var aa6 = Environment.CurrentDirectory;

            var aa7 = Environment.CurrentManagedThreadId;


            var contents = new List<Content>();
            for (int i = 0; i < 50; i++)
            {
                contents.Add(new Content { Id = i, Title = $"第{i}条数据标题测试", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now.AddDays(-i) });
            }


            //SessionHelper.SetSession("sessionKey", "内容22222222222222");             
            //var cc=  SessionHelper.GetSession("sessionKey");
            ////var currentUser = SessionHelper.GetSession<UserCacheModel>(WebConstant.SessionKey.UserCacheModel); 
            //var UserCacheModel = HttpContext.Session.GetObjectFromJson<UserCacheModel>(WebConstant.SessionKey.UserCacheModel);

            //HttpContext.Session.SetString("param", "测试测试sessionKey");
            //var value = HttpContext.Session.GetString("param");

             
            RetryPolicyHelper.Retry(() => _logger.LogError("测试内容22222222222222")); 

            return View(new ContentViewModel { Contents = contents }); 
        }
 
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 5)]
        public IActionResult Privacy()
        {
            //throw new Exception("异常了");
            //var dto = new UserViewModel();

            var info = _userService.Find("admin777");

            Check.NotNull(info, nameof(info));
              
            string timestamp = _memoryCache.Set("timestamp", DateTime.Now.ToString());
             
            string timestampGet = _memoryCache.Get<string>("timestamp");
             
            string timestamp3 = _memoryCache.GetOrCreate("timestamp3", entry => { return DateTime.Now.ToString(); });
             
            string timestampGet3 = _memoryCache.Get<string>("timestamp3");
             
            var employee1 = _memoryCache.GetOrCreate("timestamp2", entry => { return _userService.Find("admin"); });


            var employee2 = _memoryCache.Get<Employee>("timestamp2");


            var _avatarCache = new SimpleMemoryCache<Employee>();

            var myAvatar = _avatarCache.GetOrCreate("UserModel", () => _userService.Find("admin"));

            var myAvatarGet = _avatarCache.GetOrCreate("UserModel",()=> new Employee());

            var contents = new List<Content>();
            for (int i = 0; i < 5; i++)
            {
                contents.Add(new Content { Id = i, Title = $"第{i}条数据标题测试", Detail = $"第{i}条数据的内容", Status = 1, Add_time = DateTime.Now.AddDays(-i) });
            } 

            var _avatarCache3 = new SimpleMemoryCache<List<Content>>();

            var myAvatar3 = _avatarCache3.GetOrCreate("ContentModel", () => contents);


            var myAvatar4 = _avatarCache3.GetOrCreate("ContentModel", () => new List<Content>());


            var pp111 = ProviderManage.MemoryCacheProvider.MemoryCache.GetOrCreate("pp1111", entry => { return _userService.Find("admin"); });


            var pp1 =  ProviderManage.MemoryCacheProvider.MemoryCache.Set("pp1", _userService.Find("admin"));

           var pp2 = ProviderManage.MemoryCacheProvider.MemoryCache.Set("pp2","pp2p22222222");

            var pp3 = ProviderManage.MemoryCacheProvider.MemoryCache.Set("pp3", contents);

             

            var ppp1 = ProviderManage.MemoryCacheProvider.MemoryCache.Get("pp1");

            var ppp2 = ProviderManage.MemoryCacheProvider.MemoryCache.Get("pp2");

            var ppp3 = ProviderManage.MemoryCacheProvider.MemoryCache.Get("pp3");
             
            var ppp4 = ProviderManage.MemoryCacheProvider.MemoryCache.Get("pp4");
              
            var employee = _userService.Find("admin");

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
        /// <summary>
        /// [ModelBinder(typeof(HashIdModelBinder))]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public EmployeeDto PostImfo(EmployeeDto dto)
        {
            var EmpInfo = new EmployeeDto()
            {
                Id = 123,
                Name = "Name",
                BankCardDisplay = "BankCardDisplay",
                MoneryDisplay = "MoneryDisplay",
                other = "MoneryDisplay"
            };

            Hashids hashids = new Hashids("key",6);//加盐

            var aa = hashids.Encode(123);

            var bb = hashids.Decode(aa).FirstOrDefault();

            return new EmployeeDto {  Name = $"原值：{bb}，加密值：{aa}",BankCardDisplay= EmpInfo.ToString() };
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
