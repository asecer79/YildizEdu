using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
{
    public class DepartmentDal: IDepartmentDal
    {

        public Department Get(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                return dbContext.Departments.Include(f => f.Faculty).FirstOrDefault(p=>p.Id==id);
            }
        }

        public List<Department> GetAll()
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                var departments = dbContext.Departments
                    .Include(d => d.Faculty);


                return departments.ToList();

            }
        }

        public Department Add(Department department)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                 dbContext.Departments.Add(department);

                 dbContext.SaveChanges();

                 return department;
            }
        }

        public Department Update(Department department)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                dbContext.Departments.Update(department);

                dbContext.SaveChanges();

                return department;
            }
        }

        public bool Delete(int id)
        {
            using (UniEduDbContext dbContext = new UniEduDbContext())
            {
                Department department = dbContext.Departments.Find(id);

                dbContext.Departments.Remove(department);
                dbContext.SaveChanges();

                return true;
            }
        }
    }
}
