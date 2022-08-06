using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

        [HttpGet("total")]
        public async Task<ActionResult<ServiceResponse<int>>> GetTotalNrOfJobOffers()
        {
            var result = new ServiceResponse<int>();

            try
            {
                int number = await _jobOfferService.GetTotalNrOfJobOffers();
                result.Data = number;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return BadRequest(result);

            }

            return Ok(result);
        }


        [HttpPost("allJobOffers")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<JobOfferDTO>>>> GetAllJobOffer([FromQuery] int pageNr, [FromQuery] int noElements , [FromBody] FilterJobOffersDTO filter)
        {

            var result = new ServiceResponse<List<JobOfferDTO>>();

            try
            {
                var jobOffers = await _jobOfferService.GetAllJobOffers(pageNr,noElements, filter);
                result.Data = jobOffers;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return BadRequest(result);

            }

            return Ok(result);
        }


        [HttpGet("allcities")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CityShortDTO>>>> GetAll()
        {
            var result = new ServiceResponse<List<CityShortDTO>>();

            try
            {
                var citiesList = await _jobOfferService.GetAllCities();
                result.Data = citiesList;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return BadRequest(result);

            }

            return Ok(result);
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
/*        [HttpPost("testasyncpost")]
        public async Task<IActionResult> TestAsyncPost([FromForm] string testString)
        {
            if(testString is null)
            {
                return null;
            }

            string result = await _jobOfferService.TestAsyncMethod(testString);
            return Ok(result);
        }*/


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

        [HttpGet("getimages/{id}")]
        public async Task<ActionResult<ServiceResponse<List<byte[]>>>> GetImages([FromRoute] int? id)
        {
            var response = new ServiceResponse<List<byte[]>>();

            if(id == null)
            {
                response.Success = false;
                response.Message = "Id from query params is null";
                return BadRequest(response);
            }
            try
            {

                response.Data = await _jobOfferService.GetImages((int)id);

                if (response.Data.Count() == 0)
                {
                    response.Success = false;
                    response.Message = "Did not find any images!";
                    return BadRequest(response);
                }

                return Ok(response);
                

                

            }
            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }

        }

        [HttpGet("getimagetest")]
        public async Task<FileResult> GetImageTest()
        {
            string folderPath = "JobOffers_Images\\2093\\dog_image2.jpg";
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(folderPathComplete, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            if (Directory.Exists(folderPathComplete))
            {
                string[] allFiles = Directory.GetFiles(folderPathComplete);
            }
                var bytes = await System.IO.File.ReadAllBytesAsync(folderPathComplete);
            return File(bytes, contentType, Path.GetFileName(folderPathComplete));
        }



        [HttpGet("getimage/{idJob}")]
        public async Task<ActionResult> GetAllImageTest([FromRoute] int? idJob, [FromQuery] int? id)
        {
            if(idJob == null || id == null || id < 0)
            {
                return BadRequest("Ids can not be null or 0!");
            }

            string folderPath = "JobOffers_Images\\" + idJob;
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);

            var provider = new FileExtensionContentTypeProvider();

            if (Directory.Exists(folderPathComplete))
            {
                string[] allFiles = Directory.GetFiles(folderPathComplete);

                for(int i = 0; i < allFiles.Length; i++)
                {
                    if(i+1 == id)
                    {
                        if (!provider.TryGetContentType(allFiles[i], out var contentType))
                        {
                            contentType = "application/octet-stream";
                        }

                        var bytes = await System.IO.File.ReadAllBytesAsync(allFiles[i]);
                        return File(bytes, contentType, Path.GetFileName(allFiles[i]));
                    }

                }
            }

            return NotFound();

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
