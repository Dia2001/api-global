using ApiGlobal.Data.Infrastructure.Implements;
using ApiGlobal.Data.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Extensions
{
    public static class RepositoryDependencyExtensition
    {
        public static IServiceCollection AddRepositoryDependencyExtensition(this IServiceCollection services)
        {


            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // xem thêm

            return services;
        }
    }
}
