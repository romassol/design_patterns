using System.Linq;
using Example_04.Homework.FirstOrmLibrary;
using Example_04.Homework.Models;
using Example_04.Homework.SecondOrmLibrary;

namespace Example_04.Homework.Clients
{
    public class UserClient
    {
        private FirstOrmAdapter firstAdapter;
        private SecondOrmAdapter secondAdapter;

        private IFirstOrm<DbUserEntity> _firstOrm1;
        private IFirstOrm<DbUserInfoEntity> _firstOrm2;

        private ISecondOrm _secondOrm;

        private bool _useFirstOrm = true;
        private IOrmAdapter _ormAdapter => _useFirstOrm ? firstAdapter as IOrmAdapter : secondAdapter;

        public (DbUserEntity, DbUserInfoEntity) Get(int userId)
        {
            var user = _ormAdapter.GetDbUserEntity(userId);
            var userInfo = _ormAdapter.GetDbUserInfoEntity(userId);
            return (user, userInfo);
        }

        public void Add(DbUserEntity user, DbUserInfoEntity userInfo)
        {
            _ormAdapter.AddDbUserEntity(user);
            _ormAdapter.AddDbUserInfoEntity(userInfo);
        }

        public void Remove(int userId)
        {
            _ormAdapter.RemoveDbUserInfoEntity(userId);
            _ormAdapter.RemoveDbUserEntity(userId);
            
        }

        public UserClient(IFirstOrm<DbUserEntity> dbUserEntity, IFirstOrm<DbUserInfoEntity> dbUserInfo,
            ISecondOrm secondOrm)
        {
            firstAdapter = new FirstOrmAdapter(dbUserEntity, dbUserInfo);
            secondAdapter = new SecondOrmAdapter(secondOrm);
        }
    }
}
