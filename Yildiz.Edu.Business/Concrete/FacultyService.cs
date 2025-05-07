using System.Linq.Expressions;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.Business.Concrete
{
    public class FacultyService:IFacultyService
    {
        IFacultyDal _facultyDal;

        public FacultyService(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        public Faculty Get(Expression<Func<Faculty, bool>> filter)
        {
            return _facultyDal.Get(filter);
        }

        public IEnumerable<Faculty> GetAll(Expression<Func<Faculty, bool>> filter=null)
        {
            return _facultyDal.GetAll(filter);
        }

        public Faculty Add(Faculty entity)
        {
            return _facultyDal.Add(entity);
        }

        public Faculty Update(Faculty entity)
        {
            return _facultyDal.Update(entity);
        }

        public Faculty Delete(Faculty entity)
        {
            return _facultyDal.Delete(entity);
        }
    }
}
