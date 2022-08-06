using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Services;
using Handymand.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Handymand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;


        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserRequestDTO dto)
        {
            var result = new ServiceResponse<UserResponseDTO>();
            var key = _appSettings.Key;

            if(dto != null)
            {
                UserRequestDTO user = new UserRequestDTO();
                user.Email = result.DecryptStringAES(dto.Email, key, dto.Iv);
                user.Password = result.DecryptStringAES(dto.Password, key, dto.Iv);

                result = await _userService.Authenticate(user);

                if (result.Success == false)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            else
            {
                result.Success = false;
                result.Message = "User can not be null!";
                return BadRequest(result);
            }


        }

        [HttpPost("create")]
        public async Task<ActionResult<ServiceResponse<bool>>> Create(UserDTO user)
        {
            var key = _appSettings.Key;
            var result = new ServiceResponse<bool>();


            if (user != null)
            {
                var dto = new UserDTO();
                dto.Email = result.DecryptStringAES(user.Email, key, user.Iv);
                dto.FirstName = result.DecryptStringAES(user.FirstName, key, user.Iv);
                dto.LastName = result.DecryptStringAES(user.LastName, key, user.Iv);
                dto.Password = result.DecryptStringAES(user.Password, key, user.Iv);
                dto.Birthday = user.Birthday;
                try
                {
                    await _userService.CreateUser(dto);
                    result.Message = "User created!";
                    return Ok(result);
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Success = false;
                    return BadRequest(result);
                }
            }

            result.Success = false;
            result.Message = "User is null!";
            return BadRequest(result);


        }


/*        [HttpPost("profile/{id}")]
        public ActionResult<ServiceResponse<string>> GetProfile([FromBody] SkillDTO skillDTO, [FromRoute] string id)
        {
            var response = new ServiceResponse<string>();
            var key = _appSettings.Key;

            string decryptedString = response.DecryptStringAES(skillDTO.SkillName, key, skillDTO.Iv);
            byte[] Iv = response.GenerateIv();
            if (decryptedString.Contains("bun"))
            {

                response.Data = response.EncryptString("Ok,salut!", key, Iv);
                response.Iv = Iv;

            }
            else
            {
                response.Data = response.EncryptString("Ok, nu inteleg dar salut!", key, Iv);
                response.Iv = Iv;
            }

            return Ok(response);
        }*/

        [Authorization(Role.User,Role.Admin)]
        [HttpPost("getuser")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUser(DTOforIds dTOforIds) 
        {
            var response = new ServiceResponse<UserDTO>();
            var key = _appSettings.Key;

            if (dTOforIds.cryptId != null && dTOforIds.cryptId != "")
            {
                string decryptedId = response.DecryptStringAES(dTOforIds.cryptId,key,dTOforIds.Iv);


                try
                {
                    int id = Convert.ToInt32(decryptedId);
                    if (id == 0)
                    {
                        return BadRequest("Id used for request an user is null!");
                    }
                    else
                    {
                        response = await _userService.GetById(id);

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
            else
            {
                response.Success = false;
                response.Message = "Id can not be null or empty string!";
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
                    response = await _userService.GetProfileImage(id);


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
        [HttpPost("userprofileImage")]
        public async Task<ActionResult<ServiceResponse<byte[]>>> GetUserProfileImage([FromBody] DTOforIds dTOforIds)
        {
            var response = new ServiceResponse<byte[]>();

            try
            {
                if (dTOforIds.cryptId == null)
                {
                    return BadRequest("Id used for request an user is null!");
                }
                else
                {
                    var key = _appSettings.Key;
                    var id = response.DecryptStringAES(dTOforIds.cryptId, key,dTOforIds.Iv);

                    response = await _userService.GetProfileImage(Convert.ToInt32(id));


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
        [HttpGet("allforadmin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UserDTO>>>> GetAll()
        {
            var result = new ServiceResponse<List<UserDTO>>();

            try
            {
                var usersList = await _userService.GetAllUsers();
                result.Data = usersList;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return BadRequest(result);

            }

            return Ok(result);
        }

        [Authorization(Role.Admin)]
        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<bool>>> Update(UserDTO user)
        {
            var result = new ServiceResponse<bool>();


            if (user != null)
            {
                try
                {
                    int id = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                    if (id == 0)
                    {
                        return BadRequest("Id used for update a user is null!");
                    }

                    await _userService.UpdateUserAdmin(user);
                    result.Message = "User updated!";
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Success = false;
                    return BadRequest(result);
                }
            }

            return Ok(result);

        }

        [Authorization(Role.Admin)]
        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(UserDTO dto)
        {
            var result = new ServiceResponse<bool>();


            if (dto != null)
            {
                try
                {

                    await _userService.Delete(dto);
                    result.Message = "User " + dto.Email + " deleted!";
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Success = false;
                    return BadRequest(result);
                }
            }

            return Ok(result);

        }

        [Authorization(Role.Admin)]
        [HttpPut("block")]
        public async Task<ActionResult<ServiceResponse<bool>>> Block(UserDTO dto)
        {
            var result = new ServiceResponse<bool>();


            if (dto != null)
            {
                try
                {

                    await _userService.Block(dto);
                    result.Message = "User " + dto.Email + " blocked!";
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    result.Success = false;
                    return BadRequest(result);
                }
            }

            return Ok(result);

        }


    }
}
