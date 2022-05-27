﻿using Microsoft.AspNetCore.Mvc;
using WebA.Admin;
using WebA.Admin.Contracts;
using WebApiA.Attributes;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger; 
        private readonly IEmployeeContract _employeeContract;

        public TestController(ILogger<TestController> logger,
            IEmployeeContract employeeContract)
        {
            _logger = logger; 
            _employeeContract = employeeContract;
        }
        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpGet("Demo")]
        public IActionResult Get()
        {  
            var list =  _employeeContract.GetEmployees();
             
            return Ok(new { list });
        }



        /// <summary>
        /// 测试demo
        /// </summary>
        /// <returns></returns>
        [HttpPost("PostTest")]
        [PreventDoublePost]
        public IActionResult PostTest(int Id)
        {  
            return Ok(new { Id });
        }
    }
}
