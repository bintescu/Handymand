using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [HttpPost("create")]
        public IActionResult CreateJobOffer(JobOfferDTO jobOffer)
        {
            try
            {
                var result = _jobOfferService.Create(jobOffer);
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
