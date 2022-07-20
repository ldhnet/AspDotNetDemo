using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Helper;

namespace WebApiB.Code
{
    public class SendEmailConfig
    {
        #region 邮件发送队列

        public static Queue<Sys_MailInfo> MailQueue = new Queue<Sys_MailInfo>();
         
         
        #endregion
    }

}
