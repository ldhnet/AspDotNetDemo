using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Helper;
using System.Threading.Channels;

namespace WebApiB.Code
{
    public class SendEmailConfig
    {
        #region 邮件发送队列

        public static Queue<Sys_MailInfo> MailQueue = new Queue<Sys_MailInfo>();

        public static Channel<Sys_MailInfo> channel = Channel.CreateUnbounded<Sys_MailInfo>();
        #endregion
    }

}
