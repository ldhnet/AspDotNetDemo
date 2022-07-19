using Lee.Cache;
using Lee.Utility.Config;
using Lee.Utility.Helper;

namespace WebApiB.Code
{
    public class SendEmailConfig
    { 
        #region 邮件发送队列

        public static Queue<Sys_MailInfo> MailQueue
        {
            get
            {
                Queue<Sys_MailInfo> mailQueue = MailQueueSimple;
                if (mailQueue == null)
                {
                    MailQueueSimple = mailQueue = new Queue<Sys_MailInfo>();
                }
                return mailQueue;
            }
        }

        public static Queue<Sys_MailInfo> MailQueueSimple
        {
            get
            { 
                return CacheFactory.Cache.GetCache<Queue<Sys_MailInfo>>("MailQueue");
            }
            set
            {
                CacheFactory.Cache.SetCache("MailQueue",value); 
            }
        } 
        #endregion
    }

}
