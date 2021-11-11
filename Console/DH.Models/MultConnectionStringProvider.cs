using Framework.Utility.Models; 
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DH.Models
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
