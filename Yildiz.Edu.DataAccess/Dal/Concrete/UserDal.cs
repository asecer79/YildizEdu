using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
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

        public List<User> GetAll()
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User Add(User entity)
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
