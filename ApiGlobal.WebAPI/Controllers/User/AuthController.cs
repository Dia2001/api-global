using ApiGlobal.Common;
using ApiGlobal.DTO.Response;
using ApiGlobal.DTO.User;
using ApiGlobal.Service.Extensions;
using ApiGlobal.Service.Interfaces;
using ApiGlobal.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiGlobal.WebAPI.Controllers.User
{
    [ApiController]
    [Route("Api/v1/[controller]s/")]
    public class AuthController : ControllerApiBase<AuthController>
    {

        private readonly IAuthService _authService;
        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService
            ) : base(logger)
        {
            _authService = authService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> CreateUser([FromBody] UserSignUpDTO dto)
        {
            try
            {
                #region Validate
                if (dto.Password.Length < 6)
                {
                    return BadRequest(ErrorMessage.AT_LEAST_SIX_CHARACTER_FOR_PASSWORD);
                }
                #endregion
                var result = await _authService.CreateUser(dto);
                return Success<SingleId>(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"AuthController -> CreateUser Throw Exception: {ex.Message}");
                return HandleError(ex);
            }
        }

        [HttpPost("Signin")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO dto)
        {
            try
            {
                var result = await _authService.LoginUser(dto);
                return Success<LoginSuccessDTO>(result);
            }
            catch (ServiceExeption ex)
            {
                _logger.LogInformation($"AuthController -> LoginUser Throw Exception: {ex.Message}");
                return HandleError(ex);
            }
        }
    }
}
