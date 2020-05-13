using System;
using System.Threading.Tasks;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        readonly private IAuthService _authService;
        readonly private ITokenService _tokenService;

        public AuthController(IAuthService auth, ITokenService tokenService)
        {
            _authService = auth;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDto)
        {
            await _authService.Register(userForRegisterDto);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDto) 
        {
            var user = await _authService.Login(userForLoginDto);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.CreateJwtToken(user, 7);

            return Ok(token);
        }
    }
}