using ApiGlobal.DTO.Response;
using ApiGlobal.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Service.Interfaces
{
    public interface IAuthService
    {
        Task<LoginSuccessDTO> LoginAdmin(UserLoginDTO dto);
        Task<SingleId> CreateUser(UserSignUpDTO dto);
        Task<LoginSuccessDTO> LoginUser(UserLoginDTO dto);
    }
}
