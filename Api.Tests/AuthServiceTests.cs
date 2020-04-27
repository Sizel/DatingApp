using Autofac.Extras.Moq;
using DatingApp.API.Data;
using DatingApp.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Api.Tests
{
	public class AuthServiceTests
	{
		[Fact(Skip = "test has errors")]
		public void Login_ReturnsNullIfUserIsNotRegistered()
		{
			//Func<User, bool> queryUserDelegate = u => u.Name == userForLoginDto.Name.ToLower();

			//using (var mock = AutoMock.GetLoose())
			//{
			//	mock.Mock<DataContext>()
			//		.Setup(context => context.Set<User>().FirstOrDefaultAsync(queryUserDelegate))
					
			//}
		}
	}
}
