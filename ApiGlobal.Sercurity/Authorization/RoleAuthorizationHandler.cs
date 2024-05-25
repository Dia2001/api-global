using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.Service.Extensions;
using ApiGlobal.Common;

namespace ApiGlobal.Sercurity.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private IUnitOfWork _unitOfWork { set; get; }
        public RoleAuthorizationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                throw new ServiceExeption(HttpStatusCode.Unauthorized, ErrorMessage.UNAUTHORIZED);
            }

            if (requirement.AllowedRoles == null || requirement.AllowedRoles.Any() == false)
            {
                context.Succeed(requirement);
            }
            else
            {
                Guid userId = Guid.Parse(context.User.FindFirstValue("UserId"));
                var roles = context.User.FindFirst(ClaimTypes.Role).Value;
                bool found = requirement.AllowedRoles.Any(x => roles.Contains(x));
                //bool found = _unitOfWork.UserRolesRepo.GetAll().Any(x => x.UserId == userId && requirement.AllowedRoles.Any(y => y == x.Role.Name));
                if (found) context.Succeed(requirement);
                else throw new ServiceExeption(HttpStatusCode.Forbidden, ErrorMessage.FORBIDDEN);
            }
            return Task.CompletedTask;

        }
    }
}
