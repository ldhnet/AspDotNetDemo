using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core.Data;
using Framework.Utility;
using Microsoft.Data.SqlClient;
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

            var sql = "SELECT * FROM  [dbo].[SysAccount] where AccountName = @AccountName";
            var sql2 = "SELECT * FROM  [dbo].[SysAccount]";
            SqlParameter[] parameters = {
                new SqlParameter("@AccountName", "admin"),
            };

            var aaa=   _sysAccountRepository.UnitOfWork.ExecuteSqlCommand(sql, parameters);

            var aaa2 = _sysAccountRepository.UnitOfWork.SqlQuery<SysAccount>(sql, parameters);
             
            var aaa3 = _sysAccountRepository.UnitOfWork.SqlQuery<SysAccount>(sql2, new SqlParameter[] { });

            var people = _sysAccountRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.AccountName == userName); 
            return people;
        }
        public BaseResponse<SysAccount> GetSysAccount(string userName)
        {

            var people = _sysAccountRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.AccountName == userName);



            return new BaseResponse<SysAccount>(successCode.Success, "", people ?? new SysAccount());
        }
    }
}
