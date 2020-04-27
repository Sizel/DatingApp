using Autofac.Extras.Moq;
using DatingApp.API.Controllers;
using DatingApp.API.Data;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Models;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests
{
	public class AuthControllerTests
	{
		[Fact]
		public async void Register_ReturnsBadRequest()
		{
			using (var mock = AutoMock.GetLoose())
			{
				// Arrange
				var user = new UserForRegisterDTO();

				mock.Mock<IAuthService>()
						.Setup(service => service.Register(user))
						.Throws<ArgumentException>();

				// DI happens at this step
				var controller = mock.Create<AuthController>();

				// Act
				var response = await controller.Register(user);

				// Assert
				Assert.IsType<BadRequestObjectResult>(response);
			}
		}

		[Fact]
		public async void Register_ReturnsCode201()
		{
			using (var mock = AutoMock.GetLoose())
			{
				// Arrange
				var user = new UserForRegisterDTO();

				// DI happens at this step
				var controller = mock.Create<AuthController>();

				// Act
				var response = await controller.Register(user);

				// Assert
				Assert.IsType<StatusCodeResult>(response);
				
				var statusCode = ((StatusCodeResult)response).StatusCode;
				Assert.True(statusCode == 201);
			}
		}

		[Fact]
		public async void Login_ReturnsUnauthorized()
		{
			using (var mock = AutoMock.GetLoose())
			{
				// Arrange
				var user = new UserForLoginDTO();

				// DI happens at this step
				var controller = mock.Create<AuthController>();

				// Act
				var response = await controller.Login(user);

				// Assert
				Assert.IsType<UnauthorizedResult>(response);
			}
		}

		[Fact]
		public async void Login_ReturnsOkObjectResult()
		{
			using (var mock = AutoMock.GetLoose())
			{
				// Arrange
				var user = new UserForLoginDTO();

				mock.Mock<IAuthService>()
					.Setup(service => service.Login(user))
					.Returns(() =>
					{
						return Task.FromResult(new User());
					});

				// DI happens at this step
				var controller = mock.Create<AuthController>();

				// Act
				var response = await controller.Login(user);

				// Assert
				Assert.IsType<OkObjectResult>(response);
			}
		}
	}
}
