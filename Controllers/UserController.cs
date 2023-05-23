using baseAPI.Contracts;
using baseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace baseAPI.Controllers
{
        //Authorised API endpoints for CRUD operations

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByIdAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            var result = await _userService.UpdateAsync(id, user);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}
