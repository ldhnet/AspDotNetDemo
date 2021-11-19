using Microsoft.Extensions.Configuration;

namespace Framework.EF
{
    public class MultConnectionStringProvider : IMultConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        public MultConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string ConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("DBConnectionString");
            }
        }
    }
}
