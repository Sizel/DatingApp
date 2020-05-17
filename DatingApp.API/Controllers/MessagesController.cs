using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data.DTOs.Messages;
using DatingApp.Data.Models;
using DatingApp.Data.Pagination;
using DatingApp.Data.Repos;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository messagesRepo;
        private readonly IUserRepository usersRepo;
        private readonly IMapper mapper;

        public MessagesController(IMessagesRepository messagesRepo, IUserRepository usersRepo, IMapper mapper)
        {
            this.messagesRepo = messagesRepo;
            this.usersRepo = usersRepo;
            this.mapper = mapper;
        }

        [HttpGet("{messageId}", Name ="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int messageId)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (idFromToken != userId)
            {
                return Unauthorized();
            }

            var messageFromRepo = await messagesRepo.GetMessage(messageId);
            if (messageFromRepo == null)
            {
                return NotFound();
            }

            return Ok(messageFromRepo);
        }

        [HttpGet("conv/{recipientId}")]
        public async Task<IActionResult> GetConversation(int userId, int recipientId)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idFromToken != userId)
            {
                return Unauthorized();
            }

            var conversation = await messagesRepo.GetConversation(userId, recipientId).ToListAsync();

            var conversationDto = mapper.Map<IEnumerable<MessageToReturnDto>>(conversation);

            return Ok(conversationDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesPage(int userId, [FromQuery]MessagePaginationParams messageParams)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idFromToken != userId)
            {
                return Unauthorized();
            }

            var messages = messagesRepo.GetMessagesForUser();

            var filteredMessages = await Filter.FilterMessages(messages, idFromToken, messageParams);

            var filteredAndOrderedMessages = Order.OrderMessages(filteredMessages, messageParams);

            var page = await PageList<Message>.GetPage(filteredAndOrderedMessages, messageParams.PageNumber, messageParams.PageSize);

            Response.AddPaginationHeaders(page.TotalItems, page.PageSize, page.TotalPages, page.PageNumber);

            var messagesToReturnDto = mapper.Map<IEnumerable<MessageToReturnDto>>(page);

            return Ok(messagesToReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDTO messageForCreationDto)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idFromToken != userId)
            {
                return Unauthorized();
            }

            var messageToAdd = mapper.Map<Message>(messageForCreationDto);
            messageToAdd.SenderId = idFromToken;

            var recipient = await usersRepo.Get(messageForCreationDto.RecipientId);
            if (recipient == null)
            {
                return BadRequest("No such user");
            }

            messagesRepo.Add(messageToAdd);

            await messagesRepo.SaveAll();

            return CreatedAtRoute("GetMessage", new { userId = idFromToken, messageId = messageToAdd.MessageId }, messageToAdd);
        }

    }
}