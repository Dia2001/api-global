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
    public interface IUserRepository : IRepository<User, Guid> { }
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(IDbFactory factory) : base(factory)
        {

        }
    }
}
