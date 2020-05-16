using DatingApp.Data.Pagination;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Helpers
{
	public static class HttpResponseExtensions
	{
		public static void AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
			response.Headers.Add("Access-Control-Allow-Origin", "*");
		}

		public static void AddPaginationHeaders(this HttpResponse response, 
			int totalItems, int pageSize, int totalPages, int pageNumber)
		{
			var paginationHeader = new PaginationHeader(totalItems, pageSize, totalPages, pageNumber);
			var camelCaseFormatter = new JsonSerializerSettings();
			camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
			response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
			response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
		}
	}
}
