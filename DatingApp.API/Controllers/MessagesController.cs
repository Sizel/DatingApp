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
    [Route("api/users/{requestingUserId}/[controller]")]
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
        public async Task<IActionResult> GetMessage(int requestingUserId, int messageId)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (idFromToken != requestingUserId)
            {
                return Unauthorized();
            }

            var messageFromRepo = await messagesRepo.GetMessage(messageId);
            if (messageFromRepo == null)
            {
                return NotFound();
            }

            var messageToReturn = mapper.Map<MessageToReturnDto>(messageFromRepo);
            return Ok(messageToReturn);
        }

        // bad naming
        [HttpGet("conv/{requestedUserId}")]
        public async Task<IActionResult> GetConversation(int requestingUserId, int requestedUserId)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idFromToken != requestingUserId)
            {
                return Unauthorized();
            }

            var conversation = await messagesRepo.GetConversation(requestingUserId, requestedUserId).ToListAsync();

            conversation.ForEach(m => {
                if (!m.IsRead && m.SenderId == requestedUserId)
                {
                    m.IsRead = true;
                    m.DateRead = DateTime.Now;
                }
            });

            var conversationDto = mapper.Map<IEnumerable<MessageToReturnDto>>(conversation);

            return Ok(conversationDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesPage(int requestingUserId, [FromQuery]MessagePaginationParams messageParams)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (idFromToken != requestingUserId)
            {
                return Unauthorized();
            }

            var messages = messagesRepo.GetMessagesForUser();

            var filteredMessages = Filter.FilterMessages(messages, idFromToken, messageParams);

            var filteredAndOrderedMessages = Order.OrderMessages(filteredMessages, messageParams);

            var page = await PageList<Message>.GetPage(filteredAndOrderedMessages, messageParams.PageNumber, messageParams.PageSize);

            Response.AddPaginationHeaders(page.TotalItems, page.PageSize, page.TotalPages, page.PageNumber);

            var messagesToReturnDto = mapper.Map<IEnumerable<MessageToReturnDto>>(page);

            return Ok(messagesToReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int requestingUserId, MessageForCreationDTO messageForCreationDto)
        {
            var idFromToken = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var sender = await usersRepo.GetUserWithPhotos(idFromToken);

            if (sender.Id != requestingUserId)
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

            var messageToReturn = mapper.Map<MessageToReturnDto>(messageToAdd);

            await messagesRepo.SaveAll();

            return CreatedAtRoute("GetMessage", new { requestingUserId = idFromToken, messageId = messageToAdd.MessageId }, messageToReturn);
        }

    }
}