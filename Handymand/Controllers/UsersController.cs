using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserRequestDTO user)
        {
            var response = _userService.Authenticate(user);

            if(response == null)
            {
                return BadRequest(new { Message = "Username or Password is invalid" });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create(UserRequestDTO user)
        {
            try
            {
                var response = _userService.CreateUser(user);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Problem();
            }
        }

        //Daca punem doar adnotarea Authorize inseamna ca este nevoie de token generat pentru acces la endpoint
        //Daca mai adaugam si conditii in paranteza, inseamna ca se va folosi rolul 
        //[Authorize(Roles = "Admin")]

        [Authorization(Role.Admin)]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();

                return Ok(users);

            }
            catch(Exception e)
            {
                return NoContent();
            }

        }

    }
}
