using Microsoft.AspNetCore.Mvc;

namespace MqttService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public bool Get()
        {
            MqttService.Code.MqttService.PublishData("hello lee.......");
            return true;
        }
    }
}
