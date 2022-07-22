using Handymand.Models;
using Handymand.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface IUserService
    {
        //Auth

        Task<ServiceResponse<UserResponseDTO>> Authenticate(UserRequestDTO model);

        //GetAll
        Task<IEnumerable<User>> GetAllUsers();

        //GetById

        Task<ServiceResponse<UserDTO>> GetById(int Id);

        Task<ServiceResponse<MyUserDTO>> GetMyUser(int Id);

        Task<ServiceResponse<byte[]>> GetMyUserProfileImage(int Id);

        //Create

        Task<UserDTO> CreateUser(UserDTO user);

        UserDTO ConvertToDTOForCreate(User user);

        //Update
        Task<ServiceResponse<MyUserDTO>> UpdateUser(UpdateUserDTO dto);


    }
}
