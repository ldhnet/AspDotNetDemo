namespace MvcClient.Config
{
    public class GlobalContext
    {
        public static IHostEnvironment _Environment { get; set; }
        public static IConfiguration _Configuration { get; set; }

        public static IdpClients IdpClients { get; set; } = new IdpClients(); 
    }
}
