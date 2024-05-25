using ApiGlobal.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(TKey id);

        IEnumerable<TEntity> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        void Delete(TKey id);

        void Delete(TEntity entity);

        void DeleteMulti(IEnumerable<TKey> ids);

        void DeleteMulti(IEnumerable<TEntity> entities);
    }
}
