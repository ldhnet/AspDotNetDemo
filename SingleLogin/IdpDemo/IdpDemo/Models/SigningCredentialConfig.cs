using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace IdpDemo.Models
{
    public class SigningCredentialConfig
    {
        //public static SigningCredentials CreateSigningCredential2()
        //{
        //    var signinkey = new RsaSecurityKey(RSA.Create());
        //    signinkey.KeyId = "abcdefghijklmnopqrstuvwxyz";//How to generate KeyId ??
        //    var credentials = new SigningCredentials(signinkey, SecurityAlgorithms.RsaSha256);
        //    return credentials;
        //}
        public  SigningCredentials CreateSigningCredential()
        {
            var signinkey = GetSecurityKey();
            signinkey.KeyId = "123456";
            var credentials = new SigningCredentials(signinkey, SecurityAlgorithms.RsaSha256);
            return credentials;
        }
        private SecurityKey GetSecurityKey()
        {
            return new RsaSecurityKey(GetRSACryptoServiceProvider());
        }
        private RSACryptoServiceProvider GetRSACryptoServiceProvider()
        {
            return new RSACryptoServiceProvider(2048);
        }
    }
}
