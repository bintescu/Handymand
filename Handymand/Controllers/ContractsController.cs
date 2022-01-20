using Handymand.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("allavailablecontracts")]
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
    }
}
