using Microsoft.AspNetCore.Mvc;

namespace WebApiB.Controllers
{
    [ApiController] 
    public class DemoController : ControllerBase
    {
        
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Demo/Getlist")]
        public IActionResult Get()
        {
             return Ok("hello");
        }
    }
}