using IdpDemo.Models;
using System.Security.Claims;

namespace IdpDemo.Services
{
    public interface IUserInterface
    {
        bool ValidateCredentials(string username, string password);
        SysUser FindBySubjectId(string subjectId);
        SysUser FindByUsername(string username);

        SysUser FindByExternalProvider(string provider, string userId);
        SysUser AutoProvisionUser(string provider, string userId, List<Claim> claims);
    }
}
