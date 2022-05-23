using Framework.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    public class UserRepository : BaseRepository<UserEntity, int>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { } 
        public List<UserEntity> GetAll()
        {
            return EntitiesAsNoTracking.ToList();
        }
    }
}
