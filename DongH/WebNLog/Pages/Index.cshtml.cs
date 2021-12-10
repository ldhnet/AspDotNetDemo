using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using DongH.Tool.Helper;

namespace WebNLog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger; 
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                throw new Exception(nameof(IndexModel) + "=》测试bug");
            }
            catch (Exception ex)
            {

                LogHelper.Error(ex);
            }
    

            _logger.LogInformation("111111111111");
            _logger.LogDebug("3333333333333333");
            _logger.LogWarning("444444444444444444");
            _logger.LogTrace("5555555555555555555");

             
            LogHelper.Info(nameof(IndexModel) + "=》LogHelper111111111111");             
            LogHelper.Debug(nameof(IndexModel) + "=》LogHelper3333333333333333");
            LogHelper.Warn(nameof(IndexModel) + "=》LogHelper444444444444444444");
            LogHelper.Trace(nameof(IndexModel) + "=》LogHelper5555555555555555555");

        }
    }
}