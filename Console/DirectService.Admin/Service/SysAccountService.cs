using DH.Models.DbModels;
using DirectService.Admin.Contracts;
using Framework.Core.Data;
using Framework.Utility;
using Microsoft.Data.SqlClient;

namespace DirectService.Admin.Service
{
    public class SysAccountService : ISysAccountContract
    {
        private IRepository<SysAccount, int> _sysAccountRepository;
        private IRepository<Employee, int> _userRepository;
        public SysAccountService(IRepository<SysAccount, int> sysAccountRepository, IRepository<Employee, int> userRepository)
        {
            this._sysAccountRepository = sysAccountRepository;
            this._userRepository = userRepository;
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

            var aaa = _sysAccountRepository.UnitOfWork.ExecuteSqlCommand(sql, parameters);

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


        public BaseResponse BeginTransactionTest()
        {

            SysAccount model = new SysAccount
            {
                UserId = Guid.NewGuid().ToString(),
                AccountName = "admin",
                AccountNo = "1001",
                CreateBy = "admin",
                CreateTime = DateTime.Now
            };


            Employee model2 = new Employee
            {
                Name = "admin1" + new Random().Next(1),
                BankCard = "admin1",
                EmployeeName = "1001",
                Department = 1,
                Phone = "15225074031",
                EmployeeSerialNumber = "1001",
            };

            //1.开启事务
            _sysAccountRepository.UnitOfWork.BeginTransaction();


            var aaa33 = _sysAccountRepository.Insert(model);

            var aaa55 = _userRepository.Insert(model2);

            _sysAccountRepository.UnitOfWork.Rollback();


            SysAccount model5 = new SysAccount
            {
                UserId = Guid.NewGuid().ToString(),
                AccountName = "admin66",
                AccountNo = "100166",
                CreateBy = "admin66",
                CreateTime = DateTime.Now
            };


            Employee model6 = new Employee
            {
                Name = "admin1" + new Random().Next(1),
                BankCard = "admin166",
                EmployeeName = "100166",
                Department = 1,
                Phone = "15225074031",
                EmployeeSerialNumber = "100166",
            };



            //1.开启事务
            _sysAccountRepository.UnitOfWork.BeginTransaction();


            var aaa333335 = _sysAccountRepository.Insert(model5);

            var aaa3333666 = _sysAccountRepository.EntitiesAsNoTracking.FirstOrDefault(c => c.Id == 1000);
            Check.NotNull(aaa3333666, nameof(aaa3333666));

            var aaa55553 = _userRepository.Insert(model6);

            var ret = _sysAccountRepository.UnitOfWork.Commit();


            return ret > 0 ? new BaseResponse(successCode.Success) : new BaseResponse(successCode.NoChanged);
        }

    }
}
