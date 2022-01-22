using Handymand.Models.DTOs;
using Handymand.Services;
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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _skillService.GetAll();
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
        public IActionResult Create(SkillDTO skill)
        {
            try
            {
                var result = _skillService.Create(skill);
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


        [HttpPut("update")]
        public IActionResult Update(SkillDTO skill)
        {
            try
            {
                var result = _skillService.Update(skill);
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

        [HttpDelete("delete")]
        public IActionResult Delete(SkillDTO skill)
        {
            try
            {
                var result = _skillService.Delete(skill);
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
