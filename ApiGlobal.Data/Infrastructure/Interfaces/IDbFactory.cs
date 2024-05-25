using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Data.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        BuildDbContext Init();
    }
}
