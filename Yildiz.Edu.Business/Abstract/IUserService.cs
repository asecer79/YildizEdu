using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.Business.Abstract;

public interface IUserService: ICommonDbService<User>
{
    bool CheckUserToLogin(string email, string password);
    List<OperationClaim> GetUserOperationClaims(int userId);
    User GetUserByEmail(string email, string password);
  
}