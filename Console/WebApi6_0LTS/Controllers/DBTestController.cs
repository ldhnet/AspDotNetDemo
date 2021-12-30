using DH.Models.Dtos;
using DH.Models.Entities;
using DH.Models.param;
using DirectService.Admin.Contracts;
using DirectService.Admin.Service;
using Framework.Core.Data;
using Framework.Mapper;
using Framework.Utility;
using Framework.Utility.Attributes;
using Framework.Utility.Config;
using Framework.Utility.Exceptions;
using Framework.Utility.Mapping; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBTestController : ControllerBase
    {   
        private readonly IServiceProvider _provider;
        public DBTestController(IServiceProvider provider)
        {
            _provider= provider;  
        }        
        private ILogger _logger => _provider.GetLogger(nameof(DBTestController));
        private IUserService _userService => _provider.GetRequiredService<IUserService>();
        private IUnitOfWork _unitOfWork => _provider.GetRequiredService<IUnitOfWork>();
        private ISysAccountContract _SysAccountContract => _provider.GetRequiredService<ISysAccountContract>();

        private IConfiguration _configuration => _provider.GetRequiredService<IConfiguration>();

        [HttpGet]
        [AllowAnonymous]
        [Route("GetInfoAsync")]
        public async Task<IActionResult> GetInfoAsync()
        {
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222"); 
            _logger.LogError("3333333");

           var aaa=  _configuration.GetSection("DisplayName");
             

            TResponse<EmployeeDto> res = new TResponse<EmployeeDto>();
            res.Code = 1;
            res.Message = "测试";

            var list = _userService.GetAllList();

            var info =  _userService.Find("1001");
            var dto = info.MapTo<EmployeeDto>(); 
            res.Data = dto;
            res.Total = 1;  

            return Ok(res);

            //return NotFound(res);

            //return BadRequest(res);
        }
        /// <summary>
        /// 
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
  
        public async Task<IActionResult> Get(string keyword) 
        { 
            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString);

            var model2 = new Foo()
            {
                Id = 1,
                Name = "test",
                Money = 15.0m,
                CreateBy = "ldh"
            };

            var aaa2o = model2.MapTo<FooDto>();

            var aaa2222 = aaa2o.MapTo<Foo>();

            var model3 = new Person()
            {
                ID = 1,
                Name = "test", 
                CreateTime = DateTime.Now
            };
             
            var aaaaaa=  model3.MapTo(new PersonDto());

            var aaaaaa5555 = model3.MapTo<Person, PersonDto>();

            var mode333 = model3.MapTo<PersonDto>();

            var mode3335 = mode333.MapTo<Person>();
             
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");

            var cce = ShardResource.Hello;

            //string? ApiToken = "a5f3d50ab2084821953d4d45925a042a";

            var sysModel = new SysAccount()
            {
                UserId = "test",
                AccountNo = "test",
                AccountName = "ldh", 
                CreateTime =DateTime.Now
            };

            if (!sysModel.ValidateModel())
            { 
               
            }

            //var sysIfro = _SysAccountContract.CreateInfo(sysModel);


            var entity = _userService.Find(keyword);

            //throw new Exception("测试");

            Check.NotNull(entity, nameof(entity));

         

            //var aaadto= Mapper.Map<FooDto>(model2);

            var aaa666333 = _userService.CheckExistsById("test");

            Pagination page = new Pagination();
            EmployeeParam param = new EmployeeParam();
            param.EmployeeName = "test";


            var aaa8 = await _userService.GetPageList(param,page);

            


            Pagination page2 = new Pagination() {
                PageSize = 3
            };
            var aaa99 = await _userService.GetPageList(param, page2);


            var aaa11111 = await _userService.GetPageList(page);


            var aaa22222 = await _userService.GetPageList(page2);


            var aaa666 = _userService.CheckExists("1001");
            var aaa6667 = _userService.CheckExists("test");
            var aaa66687 = _userService.CheckExists("test111");
            //var aaa2o = model2.MapTo<FooDto>();

            //aaa2o.Name = "max";


            //var aaa2o3 = aaa2o.MapTo<Foo>();


            var dtoem1 = new EmployeeDetail()
            {
                EnglishName = "lilee",
                CreateTime = DateTime.Now
            };

            var logine = new EmployeeLogin()
            {

                CreateTime = DateTime.Now
            };

            var dtoem2 = new List<EmployeeLogin>()
            {
                logine 
            };

         

            var emp222 = new Employee()
            {
                Name = "test",
                BankCard = "test",
                EmployeeName = "test",
                EmployeeSerialNumber = "test",

                Department = 1,
                Phone = "test",
                WebToken = "test",

                ApiToken = "test",

                ExpirationDateUtc = DateTime.Now,
                EmployeeDetail = dtoem1,
                EmployeeLogins = dtoem2,
            };
            var empList2 = new List<Employee>();
            empList2.Add(emp222);

            TResponse res = new TResponse();
            res.Code = 1;
            res.Message = "测试";
           

            TResponse<List<Employee>> res2 = new TResponse<List<Employee>>();
            res2.Code = 0;
            res2.Message = "123456";
            res2.Data = empList2;
            res2.Total = 2;

            return Ok(res2);
            //return res2;

            // var dtoem = new EmployeeDto()
            // {
            //     Name = "test",
            //     BankCard = "test",
            //     EmployeeName = "test",
            //     EmployeeSerialNumber = "test",

            //     Department = 1,
            //     Phone = "test",
            //     WebToken = "test",

            //     ApiToken = "test",

            //     ExpirationDateUtc = DateTime.Now,
            //     EmployeeDetail=dtoem1,
            //     EmployeeLogins=dtoem2,
            // };

            //var aaa111222 = _userService.CreateInfo(dtoem);

            // var aaa10 = _userService.UpdateEmployee(new EmployeeDto());

            // var aaa9 = _userService.UpdateEmployee(new Employee());

            //var aaa8666666 = _userService.Find("1001");

            // //var aaa9 = aaa8.EmployeeDetail;
            // //var aaa10 = aaa8.EmployeeLogins; 

            // var aaa77 = _userService.GetAllList().ToList();


            // var aaa775 = aaa77.FirstOrDefault().EmployeeDetail;

            // var aaa776 = aaa77.FirstOrDefault().EmployeeLogins;

            // var aaa6 = _userService.GetAll().ToList();


            // 

            // string key = new AesHelper().Key;

            // AesHelper aes = new AesHelper();
            // string source = "admin";

            // NLogHelper.Error(source);

            // var aaaaaaaaa = HashHelper.GetMd5(source);
            // var aaaaaaaaa2 = HashHelper.GetSha1(source);

            // var aaass = aes.Encrypt(source);

            // var bbbss = aes.Decrypt(aaass);





            // var emp = new DataRepository().GetUserByToken(ApiToken) ?? new Employee();

            // //var dto = Mapper.Map<Employee>(emp);

            //var BeginTransactionTest = _SysAccountContract.BeginTransactionTest();


            // var aaa2 = _SysAccountContract.GetSysAccount("admin1");

            // var aaa = _SysAccountContract.GetSysAccountInfo("admin");
            // //return $"{aaa?.AccountName}{aaa?.AccountNo}";

            // return $"{aaa?.AccountName}{aaa?.AccountNo},{aaa2.data.AccountName}{aaa2.data.AccountNo}";
        }
         
    }
}
