﻿using Handymand.Models;
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
using System.Security.Cryptography;

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

        public UserDTO ConvertToDTOForAdminGetUser(User user)
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
            dto.Amount = user.Amount;
            dto.Blocked = user.Blocked;

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

            if (user == null)
            {
                response.Success = false;
                response.Message = "Wrong password or email!";
            }
            else if (!BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                response.Success = false;
                response.Message = "Wrong password or email!";
            }
            else if (user.Blocked == true)
            {
                response.Success = false;
                response.Message = "This user is blocked!";
            }
            else
            {
                //JWT generation (Json Web Token)

                var jwtToken = await _jwtUtils.GenerateJWTToken(user);

                response.Data = new UserResponseDTO(user, jwtToken);

            }

            return response;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var userList = await _userRepository.GetAllWithoutAdmin();

            var response = new List<UserDTO>();

            await Task.Run(() =>
            {
                foreach (var user in userList)
                {

                    var dto = ConvertToDTOForAdminGetUser(user);
                    response.Add(dto);

                }

            });

            return response;

        }


        public async Task CreateUser(UserDTO user)
        {

            var response = await _userRepository.GetByEmail(user.Email);
            if (response != null)
            {
                throw new Exception("A user with this email already exists. Use a different email.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string walletAdd = new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var animals = new List<string> { "Brown Bear", "Pink Cheetah", "Black Crocodile", "White Crocodile", "Red Fox", "White Fox", "Classic Lion", "Big Shark", "Bigger Shark", "Coral Tiger", "Cyan Whale", "Crimson Wolf", "Gray Ape", "Arabian Cobra", "Hotpink Armadillo" };
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
        }

        public async Task<ServiceResponse<UserDTO>> GetById(int Id)
        {
            var result = await _userRepository.GetById(Id);
            var response = new ServiceResponse<UserDTO>();

            if (result == null)
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


        public async Task<ServiceResponse<byte[]>> GetProfileImage(int Id)
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

            if (arr != null)
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

        public async Task<bool> UpdateUserAdmin(UserDTO dto)
        {
            if (dto.Id == null || dto.Id == 0)
            {
                throw new Exception("Can not update a user with id null or 0!");
            }

            if (dto.Email == "" || dto.Email == null)
            {
                throw new Exception("User can not be updated with empty email!");
            }

            var user = await _userRepository.FindByIdAsync((int)dto.Id);

            if (user == null)
            {
                throw new Exception("There is no user with this id!");
            }

            user.Email = dto.Email;
            user.WalletAddress = dto.WalletAddress;
            user.Address = dto.Address;
            user.Title = dto.Title;
            user.AboutMe = dto.AboutMe;

            _userRepository.Update(user);
            return await _userRepository.SaveAsync();
        }


        public async Task<bool> Delete(UserDTO dto)
        {
            if (dto.Id == null || dto.Id == 0)
            {
                throw new Exception("Can not delete a user with id null or 0!");
            }


            var user = await _userRepository.FindByIdAsync((int)dto.Id);

            if (user == null)
            {
                throw new Exception("There is no user with this id!");
            }


            _userRepository.Delete(user);

            return await _userRepository.SaveAsync();

        }

        public async Task<bool> Block(UserDTO dto)
        {
            if (dto.Id == null || dto.Id == 0)
            {
                throw new Exception("Can not block a user with id null or 0!");
            }


            var user = await _userRepository.FindByIdAsync((int)dto.Id);

            if (user == null)
            {
                throw new Exception("There is no user with this id!");
            }

            user.Blocked = !user.Blocked;

            _userRepository.Update(user);

            return await _userRepository.SaveAsync();
        }


        public async Task<ServiceResponse<MyUserDTO>> UpdateUser(UpdateUserDTO dto)
        {
            User user = await _userRepository.GetById(dto.Id);
            var response = new ServiceResponse<MyUserDTO>();

            if (user == null)
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
                    if (dto.ProfilePicture != null)
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

                foreach (string f in allFiles)
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


        public string DecryptStringAES(string encryptedValue)
        {
            var keybytes = Encoding.UTF8.GetBytes("7061737323313233");
            var iv = Encoding.UTF8.GetBytes("7061737323313233");

            //DECRYPT FROM CRIPTOJS
            var encrypted = Convert.FromBase64String(encryptedValue);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);

            return decriptedFromJavascript;
        }

        private string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        public string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}
