using BLL.Model;
using BLL.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.IsAuthenticated)
            {
                return Ok(new { result.Token, result.Expiration });
            }
            else
            {
                return BadRequest(new
                {
                    result.Message
                });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.IsAuthenticated)
            {
                return Ok(new
                {
                    result.Token,
                    result.Expiration,
                    result.UserName,
                    result.Email,
                    result.Role
                });
            }
            else
            {
                return Unauthorized(new
                {
                    result.Message
                });
            }
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleModel model)
        {
           var result = await _authService.AssignRoleAsync(model);
            if (result.IsAuthenticated)
            {
                return Ok(new
                {
                    result.Message,
                    result.UserName,
                    result.Email,
                    result.Role
                });
            }
            else
            {
                return BadRequest(new
                {
                    result.Message
                });
            }
        }

    }
}