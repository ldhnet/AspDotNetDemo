using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using WebNLog.Helper;

namespace WebNLog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Logger log = LogManager.GetCurrentClassLogger();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                throw new Exception("测试bug");
            }
            catch (Exception ex)
            {

                log.Error(ex);
                LogHelper.Error(ex);
            }
    

            _logger.LogInformation("111111111111");
            _logger.LogDebug("3333333333333333");
            _logger.LogWarning("444444444444444444");
            _logger.LogTrace("5555555555555555555");



            log.Info("LogHelper111111111111");
        
            log.Debug("LogHelper3333333333333333");
            log.Warn("LogHelper444444444444444444");
            log.Trace("LogHelper5555555555555555555");

        }
    }
}