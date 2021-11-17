using Framework.Utility.Helper;
using Framework.Utility.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class AppUnitTestBase
    {

        /// <summary>
        /// 获取 服务提供者
        /// </summary>
        protected IServiceProvider ServiceProvider { get; private set; }

        public AppUnitTestBase()
        {
            IServiceCollection services = new ServiceCollection();
    
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            IServiceProvider provider = services.BuildServiceProvider();
     
            ServiceProvider = provider;
        }

        [Fact]
        public void AesHelperTest()
        {
            string key = new AesHelper().Key; 

            AesHelper aes = new AesHelper();
            string source = "admin";

             

            var aaa = aes.Encrypt(source);

            var bbb = aes.Decrypt(source);


            //解密Ticket
            var ccc = SecurityHelper.Base64Decrypt(bbb);
        }


    }

}
