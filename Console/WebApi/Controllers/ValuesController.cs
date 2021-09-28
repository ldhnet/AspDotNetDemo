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

            _logger.LogWarning("1234567890");
            _logger.LogInformation("1234567890");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "test")]
        public string Get(int id)
        {

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
