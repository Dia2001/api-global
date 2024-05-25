using ApiGlobal.DTO.User;
using ApiGlobal.Service.Extensions;
using ApiGlobal.Service.Interfaces;
using ApiGlobal.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ApiGlobal.WebAPI.Controllers.Admin
{
    [ApiController]
    [Route("Api/v1/[controller]s/Admin")]
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

        [HttpPost("Signin")]
        public async Task<IActionResult> LoginAdmin([FromBody] UserLoginDTO dto)
        {
            try
            {
                var result = await _authService.LoginAdmin(dto);
                return Success<LoginSuccessDTO>(result);
            }
            catch (ServiceExeption ex)
            {
                _logger.LogInformation($"AuthController -> LoginAdmin Throw Exception: {ex.Message}");
                return HandleError(ex);
            }
        }
    }
}
