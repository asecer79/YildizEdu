using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.Business.Concrete
{
    public class UserService:IUserService
    {
        IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Get(int id)
        {
          return _userDal.Get(id);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User Add(User entity)
        {
           return _userDal.Add(entity);
        }

        public User Update(User entity)
        {
           return _userDal.Update(entity);
        }

        public bool Delete(int id)
        {
           return _userDal.Delete(id);
        }

        public bool CheckUserToLogin(string email, string password)
        {
             return _userDal.CheckUserToLogin(email, password);
        }

        public List<OperationClaim> GetUserOperationClaims(int userId)
        {
            return _userDal.GetUserOperationClaims(userId);
        }

        public User GetUserByEmail(string email, string password)
        {
            return _userDal.GetUserByEmail(email, password);
        }
    }
}
