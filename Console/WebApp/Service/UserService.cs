using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Context;
using WebApp.Model;

namespace WebApp.Service
{
    public class UserService : IUserService
    { 
        private IUserRepository _blogRepository;
        private ApplicationDbContext _dataContext = new ApplicationDbContext();
        public IUserRepository User
        {
            get
            {
                if (_blogRepository is null)
                {
                    _blogRepository = new UserRepository(_dataContext);
                }
                return _blogRepository;
            }
        }
         
    }
}
