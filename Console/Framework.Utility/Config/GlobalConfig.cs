namespace Framework.Utility.Config
{
    public class GlobalConfig
    {
        public static SystemConfig SystemConfig { get; set; } = new SystemConfig();
        public static MailSenderOptions MailSenderOptions { get; set; } = new MailSenderOptions();
        /// <summary>
        /// Configured service provider.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; set; }
    }
}
