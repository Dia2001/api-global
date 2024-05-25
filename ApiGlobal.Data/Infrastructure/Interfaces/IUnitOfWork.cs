using ApiGlobal.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        void SetUser(Guid userId);
        int SaveChanges();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task SaveChangesAsync();

        #region Repo
        IUserRepository UsersRepo { get; }
        IUserRoleRepository UserRolesRepo { get; }
        #endregion
    }
}
   
  
