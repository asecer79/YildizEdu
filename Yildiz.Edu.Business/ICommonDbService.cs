using Core.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Yildiz.Edu.Business
{
    public interface ICommonDbService<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null);
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
