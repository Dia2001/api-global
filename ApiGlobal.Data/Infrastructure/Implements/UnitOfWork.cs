using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbFactory _dbFactory { set; get; }
        protected BuildDbContext DbContext { get { return _dbFactory.Init(); } }
        private Guid userId { get; set; }

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public void BeginTransaction()
        {
            DbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            DbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            DbContext.Database.RollbackTransaction();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public void SetUser(Guid userId)
        {
            this.userId = userId;
        }

        #region InitAndGetRepo
        private IUserRoleRepository userRolesRepo { get; set; }
        public IUserRoleRepository UserRolesRepo => userRolesRepo ?? (userRolesRepo = new UserRoleRepository(_dbFactory));
        private IUserRepository usersRepo { get; set; }
        public IUserRepository UsersRepo => usersRepo ?? (usersRepo = new UserRepository(_dbFactory));
        #endregion
    }
}
