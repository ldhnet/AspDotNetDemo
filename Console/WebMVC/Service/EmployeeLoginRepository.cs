using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using WebMVC.Context;
using WebMVC.Model;

namespace WebMVC.Service
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class EmployeeLoginRepository : Repository<EmployeeLogin>, IEmployeeLoginRepository
    {
        private static ApplicationDbContext _dbContext = new ApplicationDbContext();

        public EmployeeLoginRepository() : this(_dbContext)
        { }

        public EmployeeLoginRepository(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        }
           
     
        public EmployeeLogin GetEmployeeLoginInfo(int Id)
        {
            return GetAsNoTracking(c=>c.Id == Id);
        }

        public List<EmployeeLogin> GetEmployeeLoginList()
        {
           return GetAll(c=> true).ToList();
        }
         
    }
}
