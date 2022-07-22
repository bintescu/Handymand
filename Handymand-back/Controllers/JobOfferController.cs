using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Handymand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }

        [HttpGet("allJobOffers")]
        public IActionResult GetAllJobOffers()
        {
            try
            {
                var result = _jobOfferService.AllJobOffers();
                return Ok(result);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Server Error"
                };
                throw new System.Web.Http.HttpResponseException(resp);

            }

        }

        [Authorization(Role.User,Role.Admin)]
        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<JobOfferDTO>>> CreateJobOffer([FromForm] JobOfferDTO jobOffer)
        {
            var result = new ServiceResponse<JobOfferDTO>();
            try
            {
                int id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                if (id == 0)
                {
                    return BadRequest("User with that id doesn`t exist!");
                }
                else
                {

                    jobOffer.IdCreationUser = id;
                    result = await _jobOfferService.Create(jobOffer);
                    if(result.Success == true)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
                return result;

            }
        }


        // Async docs
        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await
        //https://angular.io/guide/forms
        [HttpPost("testasyncpost")]
        public async Task<IActionResult> TestAsyncPost([FromForm] string testString)
        {
            if(testString is null)
            {
                return null;
            }

            string result = await _jobOfferService.TestAsyncMethod(testString);
            return Ok(result);
        }


        //Poti sa pui in documentatie si asta:
        //https://www.youtube.com/watch?v=5a6WCBftjvw
        [HttpPost("uploadfilesasync")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        public class Example
        {
            public List<IFormFile> files { get; set; }
        }
        [HttpPost("uploadfiles")]
        public IActionResult OnPostUpload([FromForm] Example dto)
        {
            long size = dto.files.Sum(f => f.Length);

            foreach (var formFile in dto.files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = dto.files.Count, size });
        }

        [HttpPost("getById")]
        public IActionResult GetJobOffer([FromBody] DTOforIds dto)
        {
            try
            {
                if(dto == null || dto.Id == null)
                {
                    throw new Exception("A fost transmis in getById din JobOfferController un dto null sau cu Id null !");
                }
                var result = _jobOfferService.GetById((int)dto.Id);
                return Ok(result);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "Server Error"
                };
                throw new System.Web.Http.HttpResponseException(resp);

            }
        }
    }
}
