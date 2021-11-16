namespace Framework.Utility.Config
{
    public class GlobalConfig
    {
        public static SystemConfig SystemConfig { get; set; } = new SystemConfig();

        /// <summary>
        /// Configured service provider.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; set; }
    }
}
