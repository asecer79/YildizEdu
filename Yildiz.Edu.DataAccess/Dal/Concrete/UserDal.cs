using Core.DataAccess.Repository.Ef;
using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User, UniEduDbContext>, IUserDal
    {
     
        public bool CheckUserToLogin(string email, string password)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Users.Any(p => p.Email == email && p.Password == password)!;
            }
        }

        public List<OperationClaim>? GetUserOperationClaims(int userId)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                var data = from oc in dbContext.OperationClaims
                           join uoc in dbContext.UserOperationClaims
                               on oc.Id equals uoc.OperationClaimId
                           where uoc.UserId == userId
                           select new OperationClaim
                           {
                               Id = oc.Id,
                               Name = oc.Name
                           };

                return data.ToList();
            }
        }

        public User GetUserByEmail(string email, string password)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Users.FirstOrDefault(p => p.Email == email && p.Password == password)!;
            }
        }
    }
}
