using System;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using DatingApp.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        readonly private IAuthService _authService;
        readonly private ITokenService _tokenService;
        readonly private IMapper _mapper;

        public AuthController(IAuthService auth, ITokenService tokenService, IMapper mapper)
        {
            _authService = auth;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDto)
        {
            var detailedUser = await _authService.Register(userForRegisterDto);

            return CreatedAtRoute("GetDetailedUser", new { controller = "Users", id = detailedUser.UserId }, detailedUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDto) 
        {
            var loggedInUser = await _authService.Login(userForLoginDto);

            if (loggedInUser == null)
                return Unauthorized();

            const int daysValid = 7;
            var token = _tokenService.CreateJwtToken(loggedInUser, daysValid);

            var userDto = _mapper.Map<UserForListDTO>(loggedInUser);

            return Ok(new
            {
                token,
                user = userDto
            });
        }
    }
}