using ApiGlobal.Data.Infrastructure.Abstracts;
using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.Model.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Repository
{
    public interface IUserRoleRepository : IRepository<UserRole, Guid> { }
    public class UserRoleRepository : RepositoryBase<UserRole, Guid>, IUserRoleRepository
    {
        public UserRoleRepository(IDbFactory factory) : base(factory)
        {

        }
    }
}
