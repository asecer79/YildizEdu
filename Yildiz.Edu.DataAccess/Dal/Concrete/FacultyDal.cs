using Core.DataAccess.Repository.Ef;
using Yildiz.Edu.DataAccess.Context;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.DataAccess.Dal.Concrete
{
    public class FacultyDal : EfEntityRepositoryBase<Faculty, UniEduDbContext>, IFacultyDal
    {
        
    }
}
