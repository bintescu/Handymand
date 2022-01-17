using Handymand.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly HandymandContext _Context;

        public TestController(HandymandContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _Context.ModelsRelations.ToListAsync();
            return Ok(result);
        }
    }
}
