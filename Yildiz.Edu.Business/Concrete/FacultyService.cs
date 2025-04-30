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

        public Faculty Get(int id)
        {
            return _facultyDal.Get(id);
        }

        public List<Faculty> GetAll()
        {
            return _facultyDal.GetAll();
        }

        public Faculty Add(Faculty entity)
        {
            return _facultyDal.Add(entity);
        }

        public Faculty Update(Faculty entity)
        {
            return _facultyDal.Update(entity);
        }

        public bool Delete(int id)
        {
            return _facultyDal.Delete(id);
        }
    }
}
