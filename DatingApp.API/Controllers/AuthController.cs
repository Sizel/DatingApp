using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using DatingApp.Data.DTOs;
using DatingApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AuthController(ITokenService tokenService, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this.tokenService = tokenService;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDto)
        {
            var userForRegister = mapper.Map<User>(userForRegisterDto);

            var result = await userManager.CreateAsync(userForRegister, userForRegisterDto.Password);
            await userManager.AddToRoleAsync(userForRegister, "Member");

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDto) 
        {
            var userFromDb = await userManager.FindByNameAsync(userForLoginDto.Username);

            if (userFromDb == null)
            {
                return Unauthorized();
            }

            var loginResult = await signInManager.CheckPasswordSignInAsync(userFromDb, userForLoginDto.Password, false);

            if (loginResult.Succeeded)
            {
                const int daysValid = 7;
                var token = tokenService.CreateJwtToken(userFromDb, daysValid);

                var userDto = mapper.Map<UserForListDTO>(userFromDb);

                return Ok(new
                {
                    token,
                    user = userDto
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}