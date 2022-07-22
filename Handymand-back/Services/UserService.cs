using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using Handymand.Utilities.JWTUtils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Handymand.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private IJWTUtils _jwtUtils;

        private IClientRepository _clientRepository;

        private IFreelancerRepository _freelancerRepository;

        private static Random random = new Random();

        public UserService(IUserRepository userRepository, IJWTUtils jWTUtils, IClientRepository clientRepository, IFreelancerRepository freelancerRepository)
        {
            _userRepository = userRepository;
            _jwtUtils = jWTUtils;
            _clientRepository = clientRepository;
            _freelancerRepository = freelancerRepository;
        }

        public UserDTO ConvertToDTOForCreate(User user)
        {
            UserDTO dto = new UserDTO();

            dto.Id = user.Id;
            dto.Email = user.Email;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Username = user.Username;

            return dto;

        }

        public UserDTO ConvertToDTOForGetUser(User user)
        {
            UserDTO dto = new UserDTO();
            dto.Id = user.Id;
            dto.Email = user.Email;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Username = user.Username;
            dto.Email = user.Email;
            dto.Address = user.Address;
            dto.AboutMe = user.AboutMe;
            dto.Phone = user.Phone;
            dto.Title = user.Title;
            dto.WalletAddress = user.WalletAddress;
            dto.DateCreated = user.DateCreated;
            dto.Birthday = user.Birthday;

            return dto;
        }

        public MyUserDTO ConvertToDTOForGetMyUser(User user)
        {
            MyUserDTO dto = new MyUserDTO();
            dto.Id = user.Id;
            dto.Email = user.Email;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Email = user.Email;
            dto.Address = user.Address;
            dto.AboutMe = user.AboutMe;
            dto.Phone = user.Phone;
            dto.Title = user.Title;
            dto.WalletAddress = user.WalletAddress;
            dto.Amount = user.Amount;
            dto.DateCreated = user.DateCreated;
            dto.Birthday = user.Birthday;
            return dto;
        }

        public async Task<ServiceResponse<UserResponseDTO>> Authenticate(UserRequestDTO model)
        {
            var response = new ServiceResponse<UserResponseDTO>();
            var user = await _userRepository.GetByEmail(model.Email);

            if(user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Wrong password or email!";
            }
            else
            {
                //JWT generation (Json Web Token)

                var jwtToken = await _jwtUtils.GenerateJWTToken(user);

                response.Data = new UserResponseDTO(user, jwtToken);

            }

            return response;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }


        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string walletAdd =  new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var animals = new List<string> { "Brown Bear", "Pink Cheetah", "Black Crocodile", "White Crocodile", "Red Fox", "White Fox", "Classic Lion", "Big Shark","Bigger Shark", "Coral Tiger", "Cyan Whale", "Crimson Wolf", "Gray Ape", "Arabian Cobra", "Hotpink Armadillo" };
            int index = random.Next(15);
            string title = animals[index];

            var userToCreate = new User()
            {
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = Role.User,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateCreated = DateTime.Now,
                WalletAddress = "xrf" + walletAdd,
                Title = title,
                Birthday = user.Birthday

            };

            await _userRepository.CreateAsync(userToCreate);

            await _userRepository.SaveAsync();

            var clientToCreate = new Client()
            {
                IdUser = userToCreate.Id,
                Location = user.Location,
                Rating = "Nou venit"

            };

            await _clientRepository.CreateAsync(clientToCreate);
            await _clientRepository.SaveAsync();

            var freelancerToCreate = new Freelancer()
            {
                IdUser = userToCreate.Id,
                Score = 0

            };

            await _freelancerRepository.CreateAsync(freelancerToCreate);
            await _freelancerRepository.SaveAsync();


            
            return ConvertToDTOForCreate(userToCreate);
        }

        public async Task<ServiceResponse<UserDTO>> GetById(int Id)
        {
            var result =  await _userRepository.GetById(Id);
            var response = new ServiceResponse<UserDTO>();

            if(result == null)
            {
                response.Success = false;
                response.Message = "No user found!";
            }
            else
            {
                response.Data = ConvertToDTOForGetUser(result);
            }


            return response;
        }

        public async Task<ServiceResponse<MyUserDTO>> GetMyUser(int Id)
        {
            var result = await _userRepository.GetById(Id);
            var response = new ServiceResponse<MyUserDTO>();

            if (result == null)
            {
                response.Success = false;
                response.Message = "No user found!";
            }
            else
            {
                response.Data = ConvertToDTOForGetMyUser(result);
            }


            return response;
        }


        public async Task<ServiceResponse<byte[]>> GetMyUserProfileImage(int Id)
        {
            var response = new ServiceResponse<byte[]>();

            string folderPath = "Images\\" + Id;
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);

            byte[] arr = null;

            if (Directory.Exists(folderPathComplete))
            {
                string[] allFiles = Directory.GetFiles(folderPathComplete);

                
                foreach (var file in allFiles)
                {

                    if (file.Contains("profile"))
                    {

                        arr = await File.ReadAllBytesAsync(file);
                        break;
                    }
                }
            }

            if(arr != null)
            {
                response.Data = arr;
            }
            else
            {
                response.Success = false;
                response.Message = "Did not find any profile image!";
            }

            return response;

        }



        public async Task<ServiceResponse<MyUserDTO>> UpdateUser(UpdateUserDTO dto)
        {
            User user = await _userRepository.GetById(dto.Id);
            var response = new ServiceResponse<MyUserDTO>();

            if(user == null)
            {
                response.Success = false;
                response.Message = "No user found!";
            }
            else
            {
                user.Email = dto.Email;
                user.Phone = dto.Phone;
                user.Address = dto.Address;
                user.Birthday = dto.Birthday.Date;
                user.AboutMe = dto.AboutMe;
                user.Title = dto.Title;

                _userRepository.Update(user);
                await _userRepository.SaveAsync();
                try
                {
                    if(dto.ProfilePicture != null)
                    await SavePictureImage(dto.ProfilePicture, dto.Id);
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }

                response.Data = ConvertToDTOForGetMyUser(user);
            }

            return response;
        }

        public async Task SavePictureImage(IFormFile file, int Id)
        {

            string folderPath = "Images\\" + Id;
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);


            Directory.CreateDirectory(folderPathComplete);

            //Aici e path-ul folderului cu numele ID-ului jobOfferului
            //Mai jos generam numele fiecarui file

            if (file.Length > 0)
            {
                string[] allFiles = Directory.GetFiles(folderPathComplete);

                foreach(string f in allFiles)
                {  
                    if (f.Contains("profile"))
                    {
                        File.Delete(f);
                    }
                }
                string[] arr = file.FileName.Split('.');

                string finalName = "profile" + "." + arr[arr.Length - 1];

                var filePath = Path.Combine(folderPathComplete, finalName);

                if (file.Length > 0)
                {

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

        }

    }
}
