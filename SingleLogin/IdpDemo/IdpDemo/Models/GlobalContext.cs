 namespace IdpDemo.Models
{
    public class GlobalContext
    {
        public static IHostEnvironment _Environment { get; set; }
        public static IConfiguration _Configuration { get; set; }

        public static IdpClients_mvc IdpClients_mvc { get; set; } = new IdpClients_mvc();
        public static IdpClients_mvc_test IdpClients_mvc_test { get; set; } = new IdpClients_mvc_test();
    }
}
