using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core.Data;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectService.Admin.Impl
{
    public class SysAccountService : ISysAccountContract
    {
        private IRepository<SysAccount, int> _sysAccountRepository;

        public SysAccountService(IRepository<SysAccount, int> sysAccountRepository)
        {
            this._sysAccountRepository = sysAccountRepository; 
        }

        public BaseResponse CreateInfo(SysAccount model)
        { 
            var ret = _sysAccountRepository.Insert(model);
            return ret > 0 ? new BaseResponse(successCode.Success) : new BaseResponse(successCode.NoChanged);
        }

        public SysAccount? GetSysAccountInfo(string userName)
        {
            var people = _sysAccountRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.AccountName == userName); 
            return people;
        }
        public BaseResponse<SysAccount> GetSysAccount(string userName)
        {
            
            var people = _sysAccountRepository.EntitiesAsNoTracking.FirstOrDefault(c=>c.AccountName== userName);

            return new BaseResponse<SysAccount>(successCode.Success,"",people??new SysAccount());
        }
    }
}
