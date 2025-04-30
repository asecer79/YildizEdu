using Yildiz.Edu.DataAccess.Dal.ICommonDbOperations;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.DataAccess.Dal.Abstract
{
    public interface  IUserDal:ICommonDal<User>
    {
       

        bool CheckUserToLogin(string email, string password);

        List<OperationClaim> GetUserOperationClaims(int userId);

        User GetUserByEmail(string email, string password);
    }
}
