using Lee.Utility.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var CurrentProcessingMonth = _context.CurrentProcessingMonth;

            var SubsidiaryID = _context.SubsidiaryID;

            return Ok(new { CurrentProcessingMonth,SubsidiaryID });
        }
    }
}
