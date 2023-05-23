using baseAPI.Contracts;
using baseAPI.Models;
using baseAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace baseAPI.Controllers
{

    //API controller for auth
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _authService.RegisterAsync(user);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(new { token = JwtHelper.GenerateToken(result.User.Id, result.User.Email) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var result = await _authService.LoginAsync(user);
            if (!result.Success)
            {
                return Unauthorized(result.ErrorMessage);
            }
            return Ok(new { token = JwtHelper.GenerateToken(result.User.Id, result.User.Email) });
        }
    }
}
