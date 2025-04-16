using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.WebUI.DataAccess.Abstract;
using Yildiz.Edu.WebUI.DataAccess.Context;
using Yildiz.Edu.WebUI.Entities.Security;

namespace Yildiz.Edu.WebUI.DataAccess.Concrete
{
    public class UserDal : IUserDal
    {
        public User Get(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Users.FirstOrDefault(p => p.Id == id)!;
            }
        }

        public IList<User> GetList()
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Create(User entity)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                dbContext.Users.Add(entity);

                dbContext.SaveChanges();

                return entity;
            }
        }

        public User Update(User entity)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                dbContext.Users.Update(entity);

                dbContext.SaveChanges();

                return entity;
            }
        }

        public bool Delete(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {

                var entity = dbContext.Users.FirstOrDefault(p => p.Id == id)!;

                dbContext.Users.Remove(entity);

                dbContext.SaveChanges();

                return true;
            }
        }

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
