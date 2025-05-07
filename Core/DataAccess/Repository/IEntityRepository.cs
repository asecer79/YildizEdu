using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Entities;

namespace Core.DataAccess.Repository
{
    public interface IEntityRepository<TEntity> where TEntity : IEntity, new()
    {
        TEntity Get(Expression<Func<TEntity,bool>> filter);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity); 
        TEntity Delete(TEntity entity);
    }
}
