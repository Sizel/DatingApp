using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDto)
        {
            User userModel = new User {
                Name = userDto.Name.ToLower()
            };

            if (await _auth.UserExists(userModel.Name))
                return BadRequest("Username already taken");

            if (await _auth.Register(userModel, userDto.Password) != null)
                return StatusCode(201);
            else
                return BadRequest();
        }
    }
}