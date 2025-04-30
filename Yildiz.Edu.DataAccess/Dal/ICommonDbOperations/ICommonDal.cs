using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.DataAccess.Dal.ICommonDbOperations
{
    public interface ICommonDal<T> where T : class
    {
        T Get(int id);
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
