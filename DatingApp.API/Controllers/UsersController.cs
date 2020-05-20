using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using DatingApp.Data.Repos;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ILikeRepository _likeRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, ILikeRepository likeRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            this._likeRepo = likeRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPage([FromQuery]UserPaginationParams userPaginationParams)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var users =  _userRepo.GetUsers();

            var filteredUsers = await Filter.FilterUsers(users, userPaginationParams, idFromToken, _userRepo);

            var filteredAndOrderedUsers = Order.OrderUsers(filteredUsers, userPaginationParams);

            var page = await PageList<User>.GetPage(filteredAndOrderedUsers, userPaginationParams.PageNumber, userPaginationParams.PageSize);

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

        [HttpPost("{likerId}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int likerId, int recipientId)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (likerId != idFromToken)
            {
                return Unauthorized();
            }

            var like = await _likeRepo.GetLike(idFromToken, recipientId);

            if (like != null)
            {
                return BadRequest("You have already liked this user");
            }

            if (await _userRepo.Get(recipientId) == null)
            {
                return BadRequest("No such user");
            }

            _likeRepo.AddLike(new Like
            {
                LikeeId = recipientId,
                LikerId = idFromToken
            });

            return Ok();
        }
    }
}