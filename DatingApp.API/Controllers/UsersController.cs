using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data.DTOs;
using DatingApp.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailedUser(int id)
        {
            var user = await _userRepo.GetDetailedUser(id);

            var detailedUser = _mapper.Map<DetailedUserDTO>(user);

            return Ok(detailedUser);
        }
    }
}