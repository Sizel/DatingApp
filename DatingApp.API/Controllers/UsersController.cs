using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using DatingApp.Data.Pagination;
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
        public async Task<IActionResult> GetUsersPage([FromQuery]PaginationParams paginationParams)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userWhoMakeRequest = await _userRepo.Get(userId);

            var users =  _userRepo.GetUsers();

            var filteredUsers = Filter.FilterUsers(users, paginationParams, userWhoMakeRequest);

            var filteredAndOrderedUsers = Order.OrderUsers(filteredUsers, paginationParams);

            var page = await PageList<User>.GetPage(filteredAndOrderedUsers, paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeaders(page.TotalItems, page.PageSize, page.TotalPages, page.PageNumber);

            var userForList = _mapper.Map<IEnumerable<UserForListDTO>>(page);

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