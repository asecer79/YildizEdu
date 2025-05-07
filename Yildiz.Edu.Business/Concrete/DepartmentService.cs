using System.Linq.Expressions;
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

        public Department Get(Expression<Func<Department, bool>> filter)
        {
            //...
            return _departmentDal.Get(filter);
        }

        public IEnumerable<Department> GetAll(Expression<Func<Department, bool>> filter=null)
        {
           return _departmentDal.GetAll(filter);
        }

        public Department Add(Department entity)
        {
           return _departmentDal.Add(entity);
        }

        public Department Update(Department entity)
        {
            return _departmentDal.Update(entity);
        }

        public Department Delete(Department entity)
        {
            return _departmentDal.Delete(entity);
        }
    }
   
}
