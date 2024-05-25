using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Implements
{
    public abstract class ServiceBase<TService> : IServiceBase
    {
        protected ILogger<TService> _logger;
        protected IUnitOfWork UnitOfWork { get; set; }
        public ServiceBase(ILogger<TService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            UnitOfWork = unitOfWork;
        }

        #region Dispose
        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _logger = null;
                _disposed = true;
            }
        }
        #endregion
    }
}
