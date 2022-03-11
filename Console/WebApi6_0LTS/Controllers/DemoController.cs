using DH.Models.Entities;
using DirectService.Admin;
using DirectService.Admin.Contracts;
using DirectService.Test.Contracts;
using Framework.Utility;
using Framework.Utility.Config;
using Framework.Utility.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi6_0.Filter;
using WebApi6_0.Models.InputDto;

namespace WebApi6_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly ITestInterface _TestInterface;
        private readonly IUserService _UserInterface;
        private readonly IEmailSender _EmailInterface;
        private readonly DataRepository _dataRepository;
        
        public DemoController(ILogger<DemoController> logger, 
            ITestInterface testIfc, 
            IUserService userIfc,
            IEmailSender emailface,
            DataRepository dataRepository
            )
        {
            _logger = logger;
            _TestInterface = testIfc;
            _UserInterface = userIfc;
            _EmailInterface = emailface;
            _dataRepository = dataRepository; 
        }
        /// <summary>
        /// 测试Demo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.Locked)]
        [AllowAnonymous]
        public IActionResult Get()
        {

            var str1111 = _dataRepository.GetUserBy_Test("呃呃呃呃呃");

            var dt1 = _dataRepository.DateTimeNow;
            var dt2 = _dataRepository.DateTimeUtc;


            Thread.Sleep(3000);

            var dt11 = _dataRepository.DateTimeNow;
            var dt22 = _dataRepository.DateTimeUtc;


            var file = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            var file2 = Directory.GetFiles(Directory.GetCurrentDirectory());

            var file3 = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory() , "wwwroot"));

            var file4 = Directory.GetFiles(GlobalConfig.RootDirectory);

            var file5 = Directory.GetFiles(GlobalConfig.wwwwroot);




            

            _logger.LogInformation(GlobalConfig.SystemConfig.DBConnectionString); 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222");
             

            var aaa = _TestInterface.TestFun();

            _logger.LogError(aaa);

            var result = _UserInterface.Find("adminaaa");

            CheckParameter.NotNullOrEmpty(aaa,"adminaaa");
            CheckParameter.Required(result,c => c != null, "dto 不能为空");

            return Ok(aaa);
        }

        /// <summary>
        /// Post测试Demo
        /// </summary>
        /// <returns></returns>  
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]DemoDto demoDto)
        { 
            _logger.LogInformation("1111111");
            _logger.LogWarning("2222222222"); 

            _logger.LogError("3333333");


            string url = $"baidu.com";
            string body =
                $"亲爱的用户 <strong>{demoDto.Name}</strong>[{demoDto.Name}]，您好！<br>"
                + $"欢迎注册，激活邮箱请 <a href=\"{url}\" target=\"_blank\"><strong>点击这里</strong></a><br>"
                + $"如果上面的链接无法点击，您可以复制以下地址，并粘贴到浏览器的地址栏中打开。<br>"
                + $"{url}<br>"
                + $"祝您使用愉快！";
            await _EmailInterface.SendEmailAsync("2283259182@qq.com", "测试邮件", body);
              
            var data = new
            {
                demoInfo = demoDto,
                deomList = new List<DemoDto>() { demoDto, demoDto },
                message = "测试"
            }; 
            return Ok(new BaseResponse<dynamic>(successCode.Success, string.Empty, data));
        }
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Upload(IFormFile file)
        {
            await Task.Run(() => { });
            var fileFolder = Path.Combine("D://D//Demo202203//MaryKay//MaryKay.SFA", "UploadFiles");

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            //var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
            //var filePath = Path.Combine(fileFolder, fileName);

            //var listData = new List<TestUser>();

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);

            //    var dt = ExcelHelper.ExcelStreamToDataTable(stream).FirstOrDefault();

            //    listData = dt.ConvertDataTableToList<TestUser>();
            //}

            return true;
        }
        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFilesAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var fileFolder = Path.Combine("D://D//Demo202203//MaryKay//MaryKay.SFA", "UploadFiles");

            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") +
                                   Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(fileFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }
    }
}
