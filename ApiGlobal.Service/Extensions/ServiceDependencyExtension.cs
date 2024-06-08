using ApiGlobal.Service.Implements;
using ApiGlobal.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Extensions
{
    public static class ServiceDependencyExtension
    {
        public static IServiceCollection AddServiceDependencyExtension(this IServiceCollection services)
        {
            services.AddSingleton<JwtService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
