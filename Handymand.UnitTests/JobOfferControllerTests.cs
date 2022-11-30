using Handymand.Controllers;
using Handymand.Data;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using Handymand.Services;
using Handymand.Utilities.JWTUtils;
using Handymand.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handymand.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Handymand.UnitTests
{
	[TestFixture]
	public class JobOfferControllerTests
	{
		private JobOfferController _controller;

		private string _key;

		private ServiceResponse<bool> _serviceResponse;

		private byte[] _iv;

		private void Login()
		{
			var claims = new List<Claim>()
			{
				new Claim("id","1")
			};

			var userIdentity = new ClaimsIdentity(claims);

			var userPrincipal = new ClaimsPrincipal(userIdentity);

			var httpContext = new DefaultHttpContext();

			httpContext.User = userPrincipal;

			_controller.ControllerContext.HttpContext = httpContext;

		}

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

			var jobOfferRepository = new JobOfferRepository(handymandContext);

			var userService = new JobOfferService(jobOfferRepository);


			_controller = new JobOfferController(userService);

		}


		[Test]
		public void JobOfferController_CreateJobOffer_ReturnJobOffer()
		{

			// Arrange
			var jobOffer = new JobOfferDTO();

			jobOffer.Description = "Test Desc";
			jobOffer.Location = "Test Location";
			jobOffer.LowPriceRange = 22;
			jobOffer.HighPriceRange = 33;
			jobOffer.Title = "Test Title";
			jobOffer.CityId = 1;

			// Act
			Login();


			var result = _controller.CreateJobOffer(jobOffer);


			// Assert
			Assert.IsTrue(result.IsCompletedSuccessfully);


		}

		[Test]
		public async Task JobOfferController_GetJobOffers_ReturnJobOffersAsExpected()
		{

			// Arrange

			var jobOffer = new JobOfferDTO();

			jobOffer.Description = "Test Desc";
			jobOffer.Location = "Test Location";
			jobOffer.LowPriceRange = 22;
			jobOffer.HighPriceRange = 33;
			jobOffer.Title = "Test Title";

			Login();


			_ = await _controller.CreateJobOffer(jobOffer);

			FilterJobOffersDTO filterJobOffersDTO = new FilterJobOffersDTO();
			// Act

			var actionResult = await _controller.GetAllJobOffer(0,2, filterJobOffersDTO);

			var result = actionResult.Result as OkObjectResult;

			var value = result.Value as ServiceResponse<List<JobOfferDTO>>;

			Assert.IsTrue(value.Data.Count > 0);
		}
	}
}
