using Handymand.Controllers;
using Handymand.Models.DTOs;
using Handymand.Models;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Handymand.Data;
using Microsoft.EntityFrameworkCore;
using Handymand.Repository.DatabaseRepositories;
using Handymand.Utilities.JWTUtils;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.Security.Claims;

namespace Handymand.UnitTests
{
	[TestFixture]
	public class UsersControllerTests
	{
		private UsersController _controller;

		private string _key;

		private ServiceResponse<bool> _serviceResponse;

		private byte[] _iv;

		[SetUp]
		public void Init()
		{
			var options = new DbContextOptionsBuilder<HandymandContext>().UseInMemoryDatabase(databaseName: "dbtest").Options;

			var handymandContext = new HandymandContext(options);


			var appSettings = Options.Create(new AppSettings());
			
			_key = "7061737323313233";

			appSettings.Value.Key = _key;

			_serviceResponse = new ServiceResponse<bool>();

			_iv = _serviceResponse.GenerateIv();

			var userRepository = new UserRepository(handymandContext);

			var jwtUtils = new Mock<IJWTUtils>();
			var clientRepo = new Mock<IClientRepository>();
			var freelancerRepo = new Mock<IFreelancerRepository>();

			var userService = new UserService(userRepository, jwtUtils.Object, clientRepo.Object, freelancerRepo.Object);


			_controller = new UsersController(userService, appSettings);

		}


		private static UserDTO UserForCreationEncrypted(string key, ServiceResponse<bool> sr, byte[] iv, UserDTO userForCreation)
		{

			userForCreation.Email = sr.EncryptString(userForCreation.Email, key, iv);
			userForCreation.FirstName = sr.EncryptString(userForCreation.FirstName, key, iv);
			userForCreation.LastName = sr.EncryptString(userForCreation.LastName, key, iv);
			userForCreation.Password = sr.EncryptString(userForCreation.Password, key, iv);
			userForCreation.Birthday = userForCreation.Birthday;
			userForCreation.Iv = iv;

			return userForCreation;
		}

		[Test]
		public void UserController_ConstructorReceiveArgument_ActAsExpected()
		{
			// Arrange

			var userService = new Mock<IUserService>();

			var appSettings = new Mock<IOptions<AppSettings>>();

			// Act
			var userController = new UsersController(userService.Object, appSettings.Object);

			// Assert

			Assert.IsTrue(userController is not null);


		}

		[Test]
		public void UserController_Create_Return200Ok()
		{
			// Arrange

			var email = "test@yahoo.com";
			var firstName = "Georgel";
			var lastName = "Vasilescu";
			var password = "parola^%@22";
			var birthday = new DateTime(1999, 2, 13);


			var userForCreation = new UserDTO();

			userForCreation.Email = _serviceResponse.EncryptString(email, _key, _iv);
			userForCreation.FirstName = _serviceResponse.EncryptString(firstName,_key, _iv);
			userForCreation.LastName = _serviceResponse.EncryptString(lastName, _key, _iv);
			userForCreation.Password = _serviceResponse.EncryptString(password, _key, _iv);
			userForCreation.Birthday = birthday;
			userForCreation.Iv = _iv;

			// Act

			var response = _controller.Create(userForCreation);


			// Assert

			Assert.That(response.IsCompletedSuccessfully == true);
		}


		[Test]
		public void UserController_Authenticate_Return200Ok()
		{

			// Arrange

			var userDTO = new UserDTO
			{
				Email = "test@yahoo.com",
				FirstName = "Georgel",
				LastName = "Vasilescu",
				Password = "123456",
				Birthday = new DateTime(1999, 2, 13)
			};

			var userForCreate = UserForCreationEncrypted(_key, _serviceResponse, _iv, userDTO);


			_ = _controller.Create(userForCreate);


			// Act
			var userForAtuh = new UserRequestDTO();

			userForAtuh.Email = _serviceResponse.EncryptString("test@yahoo.com", _key, _iv);
			userForAtuh.Password = _serviceResponse.EncryptString("123456", _key, _iv);
			userForAtuh.Iv = _iv;

			var response = _controller.Authenticate(userForAtuh);


			// Assert

			Assert.That(response.IsCompletedSuccessfully == true);
		}

		[Test]
		public void UserController_GetUser_ReturnExpectedUser()
		{
			// Arrange

			var userDTO = new UserDTO
			{
				Id = 1,
				Email = "test@yahoo.com",
				FirstName = "Georgel",
				LastName = "Vasilescu",
				Password = "123456",
				Birthday = new DateTime(1999, 2, 13)
			};

			var userForCreate = UserForCreationEncrypted(_key, _serviceResponse, _iv,userDTO);


			_ = _controller.Create(userForCreate);


			var dtoForIds = new DTOforIds();

			dtoForIds.cryptId = _serviceResponse.EncryptString("1",_key,_iv);
			dtoForIds.Iv = _iv;


			// Act

			var response = _controller.GetUser(dtoForIds);


			// Assert

			Assert.IsTrue(response.IsCompletedSuccessfully);


		}

		[Test]
		public void UserController_GetMyUser_ReturnMyUser()
		{
			// Arrange

			var userDTO = new UserDTO
			{
				Id = 1,
				Email = "test@yahoo.com",
				FirstName = "Georgel",
				LastName = "Vasilescu",
				Password = "123456",
				Birthday = new DateTime(1999, 2, 13)
			};

			var userForCreate = UserForCreationEncrypted(_key, _serviceResponse, _iv, userDTO);


			_ = _controller.Create(userForCreate);

			Login();


			// Act

			var response = _controller.GetMyUser();


			// Assert

			Assert.IsTrue(response.IsCompletedSuccessfully);


		}

		private void Login()
		{
			// START LOGIN
			var claims = new List<Claim>()
			{
				new Claim("id","1")
			};

			var userIdentity = new ClaimsIdentity(claims);

			var userPrincipal = new ClaimsPrincipal(userIdentity);

			var httpContext = new DefaultHttpContext();

			httpContext.User = userPrincipal;

			_controller.ControllerContext.HttpContext = httpContext;

			// END LOGIN
		}
	}
}
