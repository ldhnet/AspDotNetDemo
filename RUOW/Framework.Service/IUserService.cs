using Framework.EFCore.Models;

namespace Framework.Service
{
    public interface IUserService 
    {
        UserEntity GetUserEntity(int id);
        List<UserEntity> GetAll();

        bool Create(UserEntity entity);

        bool Edit(UserEntity entity);

        bool Delete(int Id);
    }
}