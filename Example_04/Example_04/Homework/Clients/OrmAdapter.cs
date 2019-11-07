using System.Linq;
using Example_04.Homework.FirstOrmLibrary;
using Example_04.Homework.Models;
using Example_04.Homework.SecondOrmLibrary;

namespace Example_04.Homework.Clients
{
    public class FirstOrmAdapter : IOrmAdapter
    {
        private readonly IFirstOrm<DbUserEntity> dbUserEntity;
        private readonly IFirstOrm<DbUserInfoEntity> dbUserInfo;

        public FirstOrmAdapter(IFirstOrm<DbUserEntity> dbUserEntity, IFirstOrm<DbUserInfoEntity> dbUserInfo)
        {
            this.dbUserEntity = dbUserEntity;
            this.dbUserInfo = dbUserInfo;
        }

        public DbUserEntity GetDbUserEntity(int userId)
        {
            return dbUserEntity.Read(userId);
        }
        
        public DbUserInfoEntity GetDbUserInfoEntity(int userId)
        {
            return dbUserInfo.Read(GetDbUserEntity(userId).InfoId);
        }

        public void AddDbUserEntity(DbUserEntity user)
        {
            dbUserEntity.Add(user);
        }

        public void AddDbUserInfoEntity(DbUserInfoEntity userInfo)
        {
            dbUserInfo.Add(userInfo);
        }

        public void RemoveDbUserEntity(int userId)
        {
            dbUserEntity.Delete(GetDbUserEntity(userId));
        }

        public void RemoveDbUserInfoEntity(int userId)
        {
            dbUserInfo.Delete(GetDbUserInfoEntity(userId));
        }
    }
    
    
    public class SecondOrmAdapter : IOrmAdapter
    {
        private readonly ISecondOrm secondOrm;

        public SecondOrmAdapter(ISecondOrm secondOrm)
        {
            this.secondOrm = secondOrm;
        }

        public DbUserEntity GetDbUserEntity(int userId)
        {
            return secondOrm.Context.Users.First(i => i.Id == userId);
        }
        
        public DbUserInfoEntity GetDbUserInfoEntity(int userId)
        {
            return secondOrm.Context.UserInfos.First(i => i.Id == userId);
        }

        public void AddDbUserEntity(DbUserEntity user)
        {
            secondOrm.Context.Users.Add(user);
        }

        public void AddDbUserInfoEntity(DbUserInfoEntity userInfo)
        {
            secondOrm.Context.UserInfos.Add(userInfo);
        }

        public void RemoveDbUserEntity(int userId)
        {
            secondOrm.Context.Users.Remove(GetDbUserEntity(userId));
        }

        public void RemoveDbUserInfoEntity(int userId)
        {
            secondOrm.Context.UserInfos.Remove(GetDbUserInfoEntity(userId));
        }
    }
}