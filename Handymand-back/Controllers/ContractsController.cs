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
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("allavailable")]
        public IActionResult GetAllAvailableContracts()
        {
            try
            {
                var result = _contractService.AllAvailableContracts();
                return Ok(result);

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        [HttpPut("update")]
        public IActionResult UpdateContract(ContractDTO contract)
        {
            try
            {
                var result = _contractService.UpdateContract(contract);
                return Ok(result);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpPost("create")]
        public IActionResult CreateContract(ContractDTO contract)
        {
            try
            {
                var result = _contractService.CreateContract(contract);
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
        public IActionResult DeleteContract(ContractDTO contract)
        {
            try
            {
                var result = _contractService.DeleteContract(contract);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
