using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
	public static class HttpResponseExtensions
	{
		public static void AddApplicationError(this HttpResponse reponse, string message)
		{
			reponse.Headers.Add("Application-Error", message);
			reponse.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
			reponse.Headers.Add("Access-Control-Allow-Origin", "*");
		}
	}
}
