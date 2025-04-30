using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yildiz.Edu.Business
{
    public interface ICommonDbService<T> where T : class
    {
        T Get(int id);
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
