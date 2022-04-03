﻿using Microsoft.AspNetCore.Mvc;
using WebA.Admin;

namespace WebApiA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private ServiceContext _context;
        public DemoController(ILogger<DemoController> logger, ServiceContext context)
        {
            _logger = logger;
            _context= context;
        }
        [HttpGet(Name = "Demo")]
        public IActionResult Get()
        {
            var CurrentMonth = _context.CurrentMonth;

            var CurrentID = _context.CurrentID;
             
            return Ok(new { CurrentMonth, CurrentID });
        }
    }
}
