using DH.Models.DbModels;
using Framework.Core.Dependency;
using Framework.Utility;

namespace DirectService.Admin.Contracts
{
    public interface ISysAccountContract: IDependency
    {
         BaseResponse CreateInfo(SysAccount model);
         SysAccount? GetSysAccountInfo(string userName);
         BaseResponse<SysAccount> GetSysAccount(string userName);
    }
}