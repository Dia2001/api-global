using ApiGlobal.Data.Infrastructure.Interfaces;
using ApiGlobal.DTO.Response;
using ApiGlobal.DTO.User;
using ApiGlobal.Model.Entities.Identity;
using ApiGlobal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using ApiGlobal.Common;
using ApiGlobal.Service.Extensions;
using System.Net;
namespace ApiGlobal.Service.Implements
{
    public class UserService : ServiceBase<UserService>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger) : base(logger, unitOfWork)
        {

        }
        public async Task<DataList<UserItemDTO>> GetAllUser(UserRequest request)
        {
            _logger.LogInformation($"UserService -> GetAllUser with query {JsonConvert.SerializeObject(request)}");
            var result = new DataList<UserItemDTO>();
            IQueryable<User> query = UnitOfWork.UsersRepo.GetAll()
                .Where(x =>
                    x.IsAdmin == false &&
                    x.Deleted == false &&
                    (request.Active != null ? x.Active : true) &&
                    (request.TypeLogin != null ? x.TYPE_LOGIN == request.TypeLogin : true) &&
                    String.IsNullOrEmpty(request.Search) ? true :
                        (
                        x.FullName.ToLower().Contains(request.Search) ||
                        x.Email.ToLower().Contains(request.Search)
                        )
                    );
            result.TotalRecord = query.Count();
            if (request.Sort != null)
            {
                switch (request.Sort)
                {
                    case 1: query = query.OrderBy(x => x.CreatedAt); break;
                    case 2: query = query.OrderByDescending(x => x.CreatedAt); break;
                    case 3: query = query.OrderBy(x => x.Email); break;
                    case 4: query = query.OrderByDescending(x => x.Email); break;
                    case 5: query = query.OrderBy(x => x.FullName); break;
                    case 6: query = query.OrderByDescending(x => x.FullName); break;
                }
            }
            else query = query.OrderByDescending(x => x.CreatedAt);
            var data = await query.Select(x => new UserItemDTO()
            {
                Id = x.Id,
                Active = x.Active,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                Deteted = x.Deleted,
                Email = x.Email,
                FullName = x.FullName,
                OAuthId = x.OAuthId,
                PhoneNumber = x.PhoneNumber,
                RefCode = x.RefCode,
                TypeLogin = x.TYPE_LOGIN,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
            }).Skip((request.Page - 1) * request.Limit).Take(request.Limit).ToListAsync();

            result.Items = data;

            _logger.LogInformation($"UserService -> GetAllUser successfully ");
            return result;
        }

        public async Task<UserItemDTO> GetUserById(Guid id)
        {
            _logger.LogInformation($"UserService -> GetUserById with Id {id}");
            var user = await UnitOfWork.UsersRepo.GetAll().Where(x => x.Id == id).Select(x => new UserItemDTO()
            {
                Id = x.Id,
                Active = x.Active,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                Deteted = x.Deleted,
                Email = x.Email,
                FullName = x.FullName,
                Avatar = x.Avatar,
                OAuthId = x.OAuthId,
                PhoneNumber = x.PhoneNumber,
                RefCode = x.RefCode,
                TypeLogin = x.TYPE_LOGIN,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
                TotalDateStudied = x.TotalDateStudied,
                DateTimeOffset = x.DateTimeOffset,
                LastTimeStudy = x.LastTimeStudy,
                TotalWords = x.TotalWords,
                Address = x.Address,
                Gender = x.Gender,
                Introduce = x.Introduce,
                IsNotify = x.IsNotify,
                TimeRemind = x.TimeRemind,


            }).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ServiceExeption(HttpStatusCode.NotFound, ErrorMessage.NOT_FOUND);
            }
            return user;
        }
    }
}
