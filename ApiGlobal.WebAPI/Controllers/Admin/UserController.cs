using ApiGlobal.Common;
using ApiGlobal.DTO.Response;
using ApiGlobal.DTO.User;
using ApiGlobal.Service.Extensions;
using ApiGlobal.Service.Interfaces;
using ApiGlobal.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGlobal.WebAPI.Controllers.Admin
{
    [Authorize]
    [ApiController]
    [Route("Api/v1/[controller]s/Admin")]
    public class UserController : ControllerApiBase<UserController>
    {
        private readonly IUserService _userService;
        public UserController(
            ILogger<UserController> logger,
            IUserService userService
            ) : base(logger)
        {
            _userService = userService;
        }

       [HttpGet("")]
       [RolesAllow(RolesName.ROLE_SYSTEM_ADMIN)]
        public async Task<IActionResult> GetAllUser([FromQuery] UserRequest request)
        {
            try
            {
                var result = await _userService.GetAllUser(request);
                return Success<DataList<UserItemDTO>>(result);
            }
            catch (ServiceExeption ex)
            {
                _logger.LogInformation($"UserController -> GetAllUser Throw Exception: {ex.Message}");
                return HandleError(ex);
            }
        }

    }
}
