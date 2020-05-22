using DatingApp.API.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Hubs
{
	public class MessageHub : Hub
	{
		public Task SendMessageToUser(string userId, string message)
		{
			return Clients.User(userId).SendAsync("ReceiveMessage", message);
		}
	}
}
