using ApiGlobal.Data.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Implements
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private IHttpContextAccessor _httpContextAccessor;
        private BuildDbContext dbContext { get; set; }
        private DbContextOptions<BuildDbContext> _options;
        public DbFactory(DbContextOptions<BuildDbContext> options, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
        }
        public BuildDbContext Init()
        {
            return dbContext ?? (dbContext = new BuildDbContext(_options, _httpContextAccessor));
        }

        public void Dispose()
        {
            if (dbContext != null) dbContext.Dispose();
        }
    }
}
