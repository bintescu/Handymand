using Handymand.Models;
using Handymand.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface IUserService
    {
        //Auth

        UserResponseDTO Authenticate(UserRequestDTO model);

        //GetAll
        IEnumerable<User> GetAllUsers();

        //GetById

        User GetById(int Id);

        //Create

        UserDTO CreateUser(UserDTO user);

        UserDTO ConvertToDTOForCreate(User user);
    }
}
