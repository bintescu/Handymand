using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Handymand.Services
{
    public class JobOfferService: IJobOfferService
    {
        private IJobOfferRepository _jobOfferRepository;
        private IUserRepository _userRepository;
        public JobOfferService(IJobOfferRepository jobOfferRepository, IUserRepository userRepository)
        {
            _jobOfferRepository = jobOfferRepository;
            _userRepository = userRepository;

        }
        
        public JobOffer ConvertFromDTO(JobOfferDTO dto)
        {
            JobOffer jobOffer = new JobOffer();

            jobOffer.Description = dto.Description;
            jobOffer.Location = dto.Location;
            jobOffer.LowPriceRange = dto.LowPriceRange;
            jobOffer.HighPriceRange = dto.HighPriceRange;
            jobOffer.Title = dto.Title;
            jobOffer.CreationUserId = dto.IdCreationUser;
            jobOffer.CityId = dto.CityId;

            return jobOffer;
        }

        public JobOfferDTO ConvertToDTO(JobOffer jobOffer)
        {
            JobOfferDTO dto = new JobOfferDTO();

            dto.IdJobOffer = jobOffer.Id;
            dto.IdCreationUser = (int)jobOffer.CreationUserId;
            if (jobOffer.CreationUser != null)
            {
                dto.FirstName = jobOffer.CreationUser.FirstName;
                dto.LastName = jobOffer.CreationUser.LastName;
                dto.Email = jobOffer.CreationUser.LastName;
            }
            dto.Description = jobOffer.Description;
            dto.Location = jobOffer.Location;
            dto.LowPriceRange = jobOffer.LowPriceRange;
            dto.HighPriceRange = jobOffer.HighPriceRange;
            dto.DateCreated = jobOffer.DateCreated;
            dto.Title = jobOffer.Title;
            dto.NoImages = jobOffer.NoImages;
            
            return dto;
        }

        public async Task<int> GetTotalNrOfJobOffers()
        {
            return await _jobOfferRepository.GetTotalNrOfJobOffers();
        }


        public async Task<List<byte[]>> GetImages(int Id)
        {
            var response = new List<byte[]>();

            string folderPath = "JobOffers_Images\\" + Id;
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);



            if (Directory.Exists(folderPathComplete))
            {
                string[] allFiles = Directory.GetFiles(folderPathComplete);


                foreach (var file in allFiles)
                {
                        byte[] arr = await File.ReadAllBytesAsync(file);
                        response.Add(arr);
                }

            }


            return response;
        }


        public async Task<List<JobOfferDTO>> GetAllJobOffers(int pageNr, int noElements, FilterJobOffersDTO filter)
        {

            
            int skip = (pageNr) * noElements;

            var initialList = await _jobOfferRepository.GetAllJobOffersInclude(skip,noElements,filter);

            var result = new List<JobOfferDTO>();

            await Task.Run(() =>
            {
                foreach (var joboffer in initialList)
                {
                    var jobdto = new JobOfferDTO()
                    {
                        IdJobOffer = joboffer.Id,
                        IdCreationUser = joboffer.CreationUserId,
                        FirstName = joboffer.CreationUser != null ? joboffer.CreationUser.FirstName : null,
                        LastName = joboffer.CreationUser != null ? joboffer.CreationUser.LastName : null,
                        Email = joboffer.CreationUser != null ? joboffer.CreationUser.Email : null,
                        Description = joboffer.Description,
                        Location = joboffer.Location,
                        Title = joboffer.Title,
                        LowPriceRange = joboffer.LowPriceRange,
                        HighPriceRange = joboffer.HighPriceRange,
                        DateCreated = joboffer.DateCreated,
                        NoImages = joboffer.NoImages
                    };

                    if(joboffer.JobOffersSkills.Count > 0)
                    {
                        jobdto.Skills = joboffer.JobOffersSkills.Select(jos => new SkillShortDTO()
                        {
                            Id = jos.IdSkill,
                            SkillName = jos.Skill.SkillName
                        }).ToList();
                    }
                    if(joboffer.City != null)
                    {
                        jobdto.City = new CityShortDTO()
                        {
                            Id = joboffer.City.Id,
                            Name = joboffer.City.Name
                        };
                    }

                    result.Add(jobdto);
                };
            });


            return result;
        }

        public async Task<ServiceResponse<JobOfferDTO>> Create(JobOfferDTO dto)
        {
            var response = new ServiceResponse<JobOfferDTO>();

            JobOffer jobOffer = ConvertFromDTO(dto);

            

            jobOffer.DateCreated = DateTime.Now;

            await _jobOfferRepository.CreateAsync(jobOffer);
            await _jobOfferRepository.SaveAsync();

            if(jobOffer.Id != 0)
            {
                response.Data = ConvertToDTO(jobOffer);
                if(dto.Files != null)
                {
                    try
                    {
                        await SaveJobOffersImages(dto.Files, jobOffer.Id);
                    }
                    catch (Exception e)
                    {
                        response.Message = e.Message;
                    }
                }

                try
                {
                    if(dto.IdSkills != null && dto.IdSkills.Count > 0)
                    {
                        await _jobOfferRepository.AddSkillsOnThisJobOfferAsync(jobOffer.Id, dto.IdSkills);

                    }
                }
                catch(Exception e)
                {
                    response.Message += "... Exception when bind joboffer with skills : " + e.Message + "\n";
                }

            }
            else
            {
                response.Success = false;
            }

            return response;
        }

        public async Task SaveJobOffersImages(List<IFormFile> files, int Id)
        {

            string folderPath = "JobOffers_Images\\" + Id;
            string currentDirectory = Directory.GetCurrentDirectory();
            var folderPathComplete = Path.Combine(currentDirectory, folderPath);


            Directory.CreateDirectory(folderPathComplete);

            //Aici e path-ul folderului cu numele ID-ului jobOfferului
            //Mai jos generam numele fiecarui file

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    var filePath = Path.Combine(folderPathComplete,formFile.FileName);

                    if (formFile.Length > 0)
                    {

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }  
                }
            }

        }

        public JobOfferDTO GetById(int Id)
        {
            var initialQuery = _jobOfferRepository.GetById(Id);

            if(initialQuery != null)
            {
                var result = ConvertToDTO(initialQuery);
                return result;
            }

            return null;


        }


        public async Task<List<CityShortDTO>> GetAllCities()
        {
            return await _jobOfferRepository.GetAllCities();
        }


    }
}
