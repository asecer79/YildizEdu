using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
{
    public class FacultyDal: IFacultyDal
    {

        public Faculty Get(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Faculties.Find(id);
            }
        }

        public List<Faculty> GetAll()
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Faculties.ToList();
            }
        }

        public Faculty Add(Faculty faculty)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                 dbContext.Faculties.Add(faculty);

                 dbContext.SaveChanges();

                 return faculty;
            }
        }

        public Faculty Update(Faculty faculty)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                dbContext.Faculties.Update(faculty);

                dbContext.SaveChanges();

                return faculty;
            }
        }

        public bool Delete(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                Faculty faculty = dbContext.Faculties.Find(id);

                dbContext.Faculties.Remove(faculty);
                dbContext.SaveChanges();

                return true;
            }
        }
    }
}
