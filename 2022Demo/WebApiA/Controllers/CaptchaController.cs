 
using Microsoft.AspNetCore.Mvc;
using SimpleCaptcha;

namespace WebApiA.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ILogger<CaptchaController> _logger;
        private readonly ICaptcha _captcha;

        public CaptchaController(ILogger<CaptchaController> logger, ICaptcha captcha)
        {
            _logger = logger;
            _captcha = captcha;
        }

        [HttpGet] 
        public IActionResult Captcha(string id)
        {
            var info = _captcha.Generate(id);
            var stream = new MemoryStream(info.CaptchaByteData);
            return File(stream, "image/gif");
        }

        [HttpGet] 
        public bool Validate(string id, string code)
        {
            if (!_captcha.Validate(id, code))
            {
                throw new Exception("无效验证码");
            }

            // 具体业务

            // 为了演示，这里仅做返回处理
            return true;
        }
    }
}
