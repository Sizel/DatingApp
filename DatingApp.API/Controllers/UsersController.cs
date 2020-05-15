using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data.DTOs;
using DatingApp.Data.Repos;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersForList()
        {
            var users = await _userRepo.GetUsersForList();

            var userForList = _mapper.Map<IEnumerable<UserForListDTO>>(users);

            return Ok(userForList);
        }

        [HttpGet("{id}", Name ="GetDetailedUser")]
        public async Task<IActionResult> GetDetailedUser(int id)
        {
            var user = await _userRepo.GetDetailedUser(id);

            var detailedUser = _mapper.Map<DetailedUserDTO>(user);

            return Ok(detailedUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdateDto)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (idFromToken != id)
            {
                return Unauthorized();
            }

            var userFromRepo = await _userRepo.GetUserWithDescr(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            await _userRepo.SaveAll();

            return NoContent();
        }
    }
}