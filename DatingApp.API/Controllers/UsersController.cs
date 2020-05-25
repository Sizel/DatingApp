using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using DatingApp.Data.Repos;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly ILikeRepository likeRepo;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepo, ILikeRepository likeRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.likeRepo = likeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPage([FromQuery]UserPaginationParams userPaginationParams)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = userRepo.GetUsers();
            var filteredUsers = await Filter.FilterUsers(users, userPaginationParams, idFromToken, userRepo);
            var filteredAndOrderedUsers = Order.OrderUsers(filteredUsers, userPaginationParams);
            var page = await PageList<User>.GetPage(filteredAndOrderedUsers, userPaginationParams.PageNumber, userPaginationParams.PageSize);
            Response.AddPaginationHeaders(page.TotalItems, page.PageSize, page.TotalPages, page.PageNumber);
            var userForList = mapper.Map<IEnumerable<UserForListDTO>>(page, opt =>
            {
                opt.Items["idFromToken"] = idFromToken;
            });

            return Ok(userForList);
        }

        [HttpGet("{id}", Name ="GetDetailedUser")]
        public async Task<IActionResult>GetDetailedUser(int id)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await userRepo.GetDetailedUser(id);
            var detailedUser = mapper.Map<DetailedUserDTO>(user, opt =>
            {
                opt.Items["idFromToken"] = idFromToken;
            });

            return Ok(detailedUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDTO userForUpdateDto)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isModerator = User.IsInRole("Moderator");
            var isAdmin = User.IsInRole("Admin");

            if (idFromToken != id && !isAdmin && !isModerator)
            {
                return Unauthorized();
            }

            var userFromRepo = await userRepo.GetUserWithDescr(id);
            mapper.Map(userForUpdateDto, userFromRepo);
            await userRepo.SaveAll();

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

            var like = await likeRepo.GetLike(idFromToken, recipientId);

            if (like != null)
            {
                return BadRequest("You have already liked this user");
            }

            if (await userRepo.Get(recipientId) == null)
            {
                return BadRequest("No such user");
            }

            likeRepo.AddLike(new Like
            {
                LikeeId = recipientId,
                LikerId = idFromToken
            });

            return Ok();
        }

        [HttpDelete("{dislikerId}/like/{recipientId}")]
        public async Task<IActionResult> DislikeUser(int dislikerId, int recipientId)
        {
            var idFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (dislikerId != idFromToken)
            {
                return Unauthorized();
            }

            var like = await likeRepo.GetLike(idFromToken, recipientId);

            if (like == null)
            {
                return BadRequest("You have already disliked this user");
            }

            if (await userRepo.Get(recipientId) == null)
            {
                return BadRequest("No such user");
            }

            likeRepo.DeleteLike(like);

            return Ok();
        }
    }
}