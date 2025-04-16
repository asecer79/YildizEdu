using Yildiz.Edu.WebUI.Entities.Security;

namespace Yildiz.Edu.WebUI.DataAccess.Abstract
{
    public interface  IUserDal
    {
        User Get(int id);
        IList<User> GetList();

        User Create(User entity);

        User Update(User entity);

        bool Delete(int id);

        bool CheckUserToLogin(string email, string password);

        List<OperationClaim> GetUserOperationClaims(int userId);

        User GetUserByEmail(string email, string password);
    }
}
