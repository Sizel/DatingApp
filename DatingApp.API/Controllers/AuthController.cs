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
        private ITokenService _tokenService;

        public AuthController(IAuthService auth, ITokenService tokenService)
        {
            _auth = auth;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userDto)
        {
            userDto.Name = userDto.Name.ToLower();

            if (await _auth.UserExists(userDto.Name))
                return BadRequest("Username already taken");

            User userModel = new User {
                Name = userDto.Name
            };

            if (await _auth.Register(userModel, userDto.Password) != null)
                return StatusCode(201);
            else
                return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userDto) 
        {
            var user = await _auth.Login(userDto.Name.ToLower(), userDto.Password);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.CreateJwtToken(user, 7);

            return Ok(new {token = token});
        }
    }
}