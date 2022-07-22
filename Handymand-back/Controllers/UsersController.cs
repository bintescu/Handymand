using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
        public async Task<IActionResult> Authenticate(UserRequestDTO user)
        {

            var response = await _userService.Authenticate(user);

            if(response.Success == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserDTO user)
        {
            try
            {
                var response = await _userService.CreateUser(user);
                return Ok(response);
            }
            catch(Exception e)
            {
                return Problem();
            }
        }

 /*       [Authorization(Role.User,Role.Admin)]
        [HttpGet("myprofile")]
        public async Task<IActionResult>*/

        [Authorization(Role.User,Role.Admin)]
        [HttpGet("getuser")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUser([FromBody] UserRequestDTO dto)
        {
            var response = new ServiceResponse<UserDTO>();
            try
            {
                int id = Convert.ToInt32(dto.Id);
                if(id == 0)
                {
                    return BadRequest("Id used for request an user is null!");
                }
                else
                {
                    response = await _userService.GetById(id);

                    if(response.Success == false)
                    {
                        return BadRequest(response);
                    }
                    else
                    {
                        return Ok(response);
                    }

                }

            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }

        }

        [Authorization(Role.User, Role.Admin)]
        [HttpGet("myuser")]
        public async Task<ActionResult<ServiceResponse<MyUserDTO>>> GetMyUser()
        {
            var response = new ServiceResponse<MyUserDTO>();
            try
            {
                int id =Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                if (id == 0)
                {
                    return BadRequest("Id used for request an user is null!");
                }
                else
                {
                    response = await _userService.GetMyUser(id);

                    if (response.Success == false)
                    {
                        return BadRequest(response);
                    }
                    else
                    {
                        return Ok(response);
                    }

                }

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }

        }



        [Authorization(Role.User, Role.Admin)]
        [HttpGet("myuserprofileImage")]
        public async Task<ActionResult<ServiceResponse<byte[]>>> GetMyUserProfileImage()
        {
            var response = new ServiceResponse<byte[]>();

            try
            {
                int id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                if (id == 0)
                {
                    return BadRequest("Id used for request an user is null!");
                }
                else
                {
                    response = await _userService.GetMyUserProfileImage(id);


                    if (response.Success == false)
                    {
                        return BadRequest(response);
                    }
                    else
                    {
                        return Ok(response);
                    }

                }

            }
            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }

        }

        [Authorization(Role.User, Role.Admin)]
        [HttpPut("updatemyuser")]
        public async Task<ActionResult<ServiceResponse<MyUserDTO>>> UpdateUser([FromForm] UpdateUserDTO dto)
        {
            var response = new ServiceResponse<MyUserDTO>();
            try
            {
                int id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                if (id == 0)
                {
                    return BadRequest("Id used for request an user is null!");
                }
                else
                {
                    dto.Id = id;
                    response = await _userService.UpdateUser(dto);

                    if (response.Success == false)
                    {
                        return BadRequest(response);
                    }
                    else
                    {
                        return Ok(response);
                    }

                }

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                return BadRequest(response);
            }

        }


        //Daca punem doar adnotarea Authorize inseamna ca este nevoie de token generat pentru acces la endpoint
        //Daca mai adaugam si conditii in paranteza, inseamna ca se va folosi rolul 
        //[Authorize(Roles = "Admin")]

        [Authorization(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                return Ok(users);

            }
            catch(Exception e)
            {
                return NoContent();
            }

        }

    }
}
