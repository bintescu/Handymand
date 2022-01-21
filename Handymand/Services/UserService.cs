using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using Handymand.Utilities.JWTUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private IJWTUtils _jwtUtils;

        private IClientRepository _clientRepository;

        private IFreelancerRepository _freelancerRepository;

        public UserService(IUserRepository userRepository, IJWTUtils jWTUtils, IClientRepository clientRepository, IFreelancerRepository freelancerRepository)
        {
            _userRepository = userRepository;
            _jwtUtils = jWTUtils;
            _clientRepository = clientRepository;
            _freelancerRepository = freelancerRepository;
        }


        public UserResponseDTO Authenticate(UserRequestDTO model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            if(user == null)
            {
                return null;
            }
            else if(BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                //JWT generation (Json Web Token)

                var jwtToken = _jwtUtils.GenerateJWTToken(user);

                return new UserResponseDTO(user, jwtToken);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User CreateUser(UserRequestDTO user)
        {
            var userToCreate = new User()
            {
                Username = user.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = Role.User
            };

            _userRepository.Create(userToCreate);

            _userRepository.Save();

            var clientToCreate = new Client()
            {
                IdUser = userToCreate.Id,
                Location = user.Location,
                Rating = "Nou venit"

            };

            _clientRepository.Create(clientToCreate);
            _clientRepository.Save();

            var freelancerToCreate = new Freelancer()
            {
                IdUser = userToCreate.Id,
                Score = 0

            };

            _freelancerRepository.Create(freelancerToCreate);
            _freelancerRepository.Save();


            
            return userToCreate;
        }

        public User GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
