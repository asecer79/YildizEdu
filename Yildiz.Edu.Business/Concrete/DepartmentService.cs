using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.Business.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        IDepartmentDal _departmentDal;

        public DepartmentService(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public Department Get(int id)
        {
            //...
            return _departmentDal.Get(id);
        }

        public List<Department> GetAll()
        {
           return _departmentDal.GetAll();
        }

        public Department Add(Department entity)
        {
           return _departmentDal.Add(entity);
        }

        public Department Update(Department entity)
        {
            return _departmentDal.Update(entity);
        }

        public bool Delete(int id)
        {
            return _departmentDal.Delete(id);
        }
    }
   
}
