using DH.Models.Entities;
using Framework.Core.Dependency;
using Framework.Utility.Config;
using Framework.Utility.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectService.Admin
{
    public class DataRepository : IDependency
    {
        public DateTime DateTimeNow { get { return DateTime.Now; } }

        public DateTime DateTimeUtc { get { return DateTime.UtcNow; } }

        public string GetUserBy_Test(string keyword)
        {
            return $"调用成功_{keyword}";
        }
        public Employee? GetUserByToken(string token)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT Id
                                  ,[Name]
                                  ,[BankCard]
                                  ,[EmployeeName]
                                  ,[Department]
                                  ,[Phone]
                                  ,[EmployeeSerialNumber]
                                  ,[WebToken]
                                  ,[ApiToken]
                            FROM    Employee 
                            WHERE   WebToken = '" + token + "' or ApiToken = '" + token + "'");

            return new SqlHelper(GlobalConfig.SystemConfig.DBConnectionString).GetEntites<Employee>(strSql.ToString()).FirstOrDefault();

        }
    }
}
