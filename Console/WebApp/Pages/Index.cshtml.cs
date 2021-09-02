using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.Model;
using WebApp.Service;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 
        private readonly IOptions<SystemConfig> _options;
        private readonly IUserService _userService;
        private readonly IEmployeeLoginRepository _loginRepository;
        public IndexModel(ILogger<IndexModel> logger, IOptions<SystemConfig> options, IUserService userService)
        {
            _logger = logger;
            _options = options;
            _userService = userService; 
        }
          
        public void OnGet()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Debug.Assert(context is not null, "123456789");
            _logger.LogError("1111");
            _logger.LogInformation("1111");
            _logger.LogWarning("1111");
            //EmployeeLogin zhangsan = new EmployeeLogin { CreateTime = DateTime.Now }; 
            //context.EmployeeLogins.Add(zhangsan);
            //context.SaveChanges();

            Expression<Func<EmployeeLogin, bool>> expression = c => c.Id == 1;

            RepositoryFactory factory =new RepositoryFactory();     

             var aaaa=  factory.BaseRepository().FindEntity<EmployeeLogin>(1);


            //Employee zhangsan = new Employee { Name = "张三", BankCard = "12345", BankCardDisplay = "12345", Monery = "11.1", MoneryDisplay = "11.1" };
            //Employee lisi = new Employee { Name = "李四", BankCard = "67890", BankCardDisplay = "67890", Monery = "12.2", MoneryDisplay = "11.1" };
            //context.Employees.AddRange(zhangsan, lisi);
            //context.SaveChanges();

          //  var list3 = _loginRepository.GetEmployeeLoginList();

            //var list1 = _userService.User.GetAsNoTracking(c=>c.Name == "张三");
            //var list2 = _userService.User.GetEmployeeInfo("张三");
            //      var list = context.Employees.ToList();

        }
    }
}
