using IdentityModel;
using IdpDemo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdpDemo.Services
{
    public class UserService: IUserInterface
    { 
        public bool ValidateCredentials(string username, string password)
        {
            SysUser testUser = FindByUsername(username);
            if (testUser != null)
            {
                if (string.IsNullOrWhiteSpace(testUser.Password) && string.IsNullOrWhiteSpace(password))
                {
                    return true;
                }

                return testUser.Password.Equals(password);
            }

            return false;
        }
         
        public SysUser FindBySubjectId(string subjectId)
        {
            var testUser = TestUsers.Users.FirstOrDefault(c => c.SubjectId == subjectId);
            var _user = new SysUser()
            {
                SubjectId = testUser?.SubjectId,
                Username = testUser?.Username,
                Password = testUser?.Password,
                ProviderName = testUser?.ProviderName,
                ProviderSubjectId = testUser?.ProviderSubjectId,
                Claims = testUser?.Claims,
            };

            return _user;
        }
         
        public SysUser FindByUsername(string username)
        { 
            var testUser = TestUsers.Users.FirstOrDefault(c => c.Username == username);
            var _user = new SysUser() {
                SubjectId = testUser?.SubjectId,
                Username = testUser?.Username,
                Password=testUser?.Password,
                ProviderName = testUser?.ProviderName,
                ProviderSubjectId = testUser?.ProviderSubjectId,
                Claims = testUser?.Claims, 
            };
            return _user;
        }
         
        public SysUser FindByExternalProvider(string provider, string userId)
        {
            var testUser = TestUsers.Users.FirstOrDefault(x => x.ProviderName == provider && x.ProviderSubjectId == userId);
            var _user = new SysUser()
            {
                SubjectId = testUser?.SubjectId,
                Username = testUser?.Username,
                Password = testUser?.Password,
                ProviderName = testUser?.ProviderName,
                ProviderSubjectId = testUser?.ProviderSubjectId,
                Claims = testUser?.Claims,
            };
            return _user; 
        }

  
        public SysUser AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            List<Claim> list = new List<Claim>();
            foreach (Claim claim in claims)
            {
                if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                {
                    list.Add(new Claim("name", claim.Value));
                }
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    list.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                else
                {
                    list.Add(claim);
                }
            }

            if (!list.Any((Claim x) => x.Type == "name"))
            {
                string text = list.FirstOrDefault((Claim x) => x.Type == "given_name")?.Value;
                string text2 = list.FirstOrDefault((Claim x) => x.Type == "family_name")?.Value;
                if (text != null && text2 != null)
                {
                    list.Add(new Claim("name", text + " " + text2));
                }
                else if (text != null)
                {
                    list.Add(new Claim("name", text));
                }
                else if (text2 != null)
                {
                    list.Add(new Claim("name", text2));
                }
            }

            string text3 = CryptoRandom.CreateUniqueId(32, CryptoRandom.OutputFormat.Hex);
            string username = list.FirstOrDefault((Claim c) => c.Type == "name")?.Value ?? text3;
            SysUser testUser = new SysUser
            {
                SubjectId = text3,
                Username = username,
                ProviderName = provider,
                ProviderSubjectId = userId,
                Claims = list
            }; 
            return testUser;
        }
    }
}
