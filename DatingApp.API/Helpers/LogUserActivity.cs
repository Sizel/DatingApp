using DatingApp.Data.Repos;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Helpers
{
	public class LogUserActivity : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var actionContext = await next();

			var idFromToken = int.Parse(actionContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

			var repo = actionContext.HttpContext.RequestServices.GetService<IUserRepository>();

			var user = await repo.Get(idFromToken);

			user.LastActive = DateTime.Now;

			await repo.SaveAll();
		}
	}
}
