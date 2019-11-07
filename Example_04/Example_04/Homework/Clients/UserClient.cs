using System.Linq;
using Example_04.Homework.FirstOrmLibrary;
using Example_04.Homework.Models;
using Example_04.Homework.SecondOrmLibrary;

namespace Example_04.Homework.Clients
{
    public class UserClient
    {
        private readonly FirstOrmAdapter firstAdapter;
        private readonly SecondOrmAdapter secondAdapter;

        private bool _useFirstOrm = true;
        private IOrmAdapter OrmAdapter => _useFirstOrm ? firstAdapter as IOrmAdapter : secondAdapter;

        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            var user = OrmAdapter.GetDbUserEntity(userId);
            var userInfo = OrmAdapter.GetDbUserInfoEntity(userId);
            return (user, userInfo);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            OrmAdapter.AddDbUserEntity(user);
            OrmAdapter.AddDbUserInfoEntity(userInfo);
        }

        public void Remove(int userId)
        {
            OrmAdapter.RemoveDbUserInfoEntity(userId);
            OrmAdapter.RemoveDbUserEntity(userId);
            
        }

        public UserClient(IFirstOrm<DbUserEntity> dbUserEntity, IFirstOrm<DbUserInfoEntity> dbUserInfo,
            ISecondOrm secondOrm)
        {
            firstAdapter = new FirstOrmAdapter(dbUserEntity, dbUserInfo);
            secondAdapter = new SecondOrmAdapter(secondOrm);
        }
    }
}
