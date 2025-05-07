using Core.DataAccess.Repository.Ef;
using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
{
    public class DepartmentDal: EfEntityRepositoryBase<Department, UniEduDbContext>, IDepartmentDal
    {

   
    }
}
