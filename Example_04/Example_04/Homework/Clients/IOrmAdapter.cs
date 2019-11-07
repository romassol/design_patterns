using Example_04.Homework.Models;

namespace Example_04.Homework.Clients
{
    public interface IOrmAdapter // ITarget
    {
        DbUserEntity GetDbUserEntity(int userId);
        DbUserInfoEntity GetDbUserInfoEntity(int userId);
        void AddDbUserEntity(DbUserEntity user);
        void AddDbUserInfoEntity(DbUserInfoEntity userInfo);
        void RemoveDbUserEntity(int userId);
        void RemoveDbUserInfoEntity(int userId);
    }
}
