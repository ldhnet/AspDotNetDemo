using Framework.EFCore.Models;
using Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserEntity GetUserEntity(int id)
        {
            var info = _userRepository.GetByKey(id);
            return info;
        }
        public List<UserEntity> GetAll()
        {
            var list = _userRepository.GetAll();
            return list;
        }

        public bool Create(UserEntity entity)
        {
            var result = _userRepository.Insert(entity);
            return result > 0;
        }

        public bool Edit(UserEntity entity)
        {
            var result = _userRepository.Update(entity);
            return result > 0;
        }

        public bool Delete(int Id)
        {
            var result = _userRepository.Delete(Id);
            return result > 0;
        }
    }
}
