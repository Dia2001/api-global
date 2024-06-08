using ApiGlobal.DTO.Response;
using ApiGlobal.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserItemDTO> GetUserById(Guid id);
        Task<DataList<UserItemDTO>> GetAllUser(UserRequest request);
    }
}
