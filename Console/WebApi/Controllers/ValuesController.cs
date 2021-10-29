using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Handler;
using WebApi.Code;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }
        // GET api/values
        [Description("测试get")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthenticateHandler.SchemeName, Roles = "test")]//AuthenticateHandler.SchemeName ，包括Cookies, JwtBearer, OAuth, OpenIdConnect等。
        public IEnumerable<string> Get()
        {
            ApiDbContext context = new ApiDbContext();

            Employee zhangsan = new Employee { Name = "张三", BankCard = "12345" };
            Employee lisi = new Employee { Name = "李四", BankCard = "67890" };
            context.Employees.AddRange(zhangsan, lisi);
            context.SaveChanges();

            var list = context.Employees.ToList();

            _logger.LogWarning("/*********1*********/");

            foreach (var item in list)
            {
                _logger.LogInformation($"{item.Id}{item.Name}{item.BankCard}");
            } 
            _logger.LogWarning("/*********2*********/");
             

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Description("测试getbyId")]
        [HttpGet("{id}")]
        [AllowAnonymous]
        //[Authorize(Roles = "test")]
        public string Get(int id)
        {

            return "value";
        }

        // POST api/values+
        /// <summary>
        /// 测试post
        /// </summary>
        /// <param name="value"></param>
        [Description("测试post")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _logger.LogWarning("测试post=" + value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _logger.LogWarning("测试Put:id = " +id + " value="  + value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
{
            _logger.LogWarning("测试Delete=" + id);
        }
    }
}
