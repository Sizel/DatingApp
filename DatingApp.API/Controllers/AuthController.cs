using System.Threading.Tasks;
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
        public async Task<IActionResult> Register(string username, string password)
        {
            username = username.ToLower();

            User userToAdd = new User {
                Name = username
            };

            if (await _auth.Register(userToAdd, password) == null)
                return BadRequest("Username already taken");
            else
                return StatusCode(201);
        }
    }
}