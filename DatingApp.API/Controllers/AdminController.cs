using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Models;
using DatingApp.Data.DTOs;
using DatingApp.Data.DTOs.Users;
using DatingApp.Data.Models;
using DatingApp.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AdminController(IUserRepository userRepository, IMapper mapper,
            UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("usersWithRoles")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = userRepository.GetUsers().Include(u => u.UserRoles).ThenInclude(ur => ur.Role);
            var usersWithRolesDto = mapper.Map <ICollection<UserWithRolesDTO>>(users);
            return Ok(usersWithRolesDto);
        }

        [HttpPost("editRoles/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EditRoles(int id, RolesForEditDTO rolesForEditDto)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            var userRoles = await userManager.GetRolesAsync(user);

            var result = await userManager.AddToRolesAsync(user, rolesForEditDto.Roles.Except(userRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to add roles");
            }

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(rolesForEditDto.Roles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove roles");
            }

            return Ok(await userManager.GetRolesAsync(user));
        }

        [HttpGet("photosForModeration")]
        [Authorize(Policy = "RequireAdminOrModeratorRole")]
        public async Task<IActionResult> GetPhotosForModeration()
        {
            return Ok("Admin or moderators can see this");
        }
    }
}