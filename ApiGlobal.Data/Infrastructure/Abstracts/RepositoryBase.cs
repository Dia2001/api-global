using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.Model.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Abstracts
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private IDbFactory _dbFactory { get; set; }
        private BuildDbContext _dbContext { get { return _dbFactory.Init(); } }
        private DbSet<TEntity> DbSet { get; set; }
        public RepositoryBase(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            DbSet = _dbContext.Set<TEntity>();
        }
        public void Delete(TKey id)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteMulti(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                var entity = DbSet.Find(id);
                DbSet.Remove(entity);
            }
        }

        public void DeleteMulti(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Where(i => ids.Contains(i.Id));
        }

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Add(entity);
            }
        }

        public void Update(TEntity entity) //xem kỹ lại phần này
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            DbSet.Attach(entity);
            EntityEntry<TEntity> entry = _dbContext.Entry(entity);
            foreach (var selector in properties)
            {
                entry.Property(selector).IsModified = true;
            }
        }
    }
}
