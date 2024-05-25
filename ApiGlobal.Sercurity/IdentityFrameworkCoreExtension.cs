using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiGlobal.Data;
using ApiGlobal.Model.Entities.Identity;
using ApiGlobal.Sercurity.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGlobal.Sercurity
{
    public static class IdentityFrameworkCoreExtension
    {
        public static IServiceCollection AddIdentityFrameworkCore(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<BuildDbContext>();
            services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
            return services;
        }
    }
}
