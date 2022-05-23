using Framework.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Repository
{
    public class Repository : BaseRepository<UserEntity, int>
    {
        public Repository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
