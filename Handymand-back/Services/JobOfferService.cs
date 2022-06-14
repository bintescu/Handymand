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

            jobOffer.CreationUser = _userRepository.FindById(dto.IdCreationUser);
            jobOffer.DateCreated = dto.DateCreated;
            jobOffer.Description = dto.Description;
            jobOffer.Location = dto.Location;
            jobOffer.LowPriceRange = dto.LowPriceRange;
            jobOffer.HighPriceRange = dto.HighPriceRange;
            jobOffer.Title = dto.Title;

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
            
            return dto;
        }
        public List<JobOfferDTO> AllJobOffers()
        {
            var initialQuery =  _jobOfferRepository.GetAllAsQueryable();

            var result = initialQuery.Select(i => new JobOfferDTO() 
            {
                IdJobOffer = i.Id,
                FirstName = i.CreationUser != null ? i.CreationUser.FirstName : null,
                LastName = i.CreationUser != null ? i.CreationUser.LastName : null,
                Email = i.CreationUser != null ? i.CreationUser.Email : null,
                Description = i.Description,
                Location = i.Location,
                Title = i.Title,
                LowPriceRange = i.LowPriceRange,
                HighPriceRange = i.HighPriceRange,
                DateCreated = i.DateCreated
            }).ToList();

            return result;
        }

        public async Task<JobOfferDTO> Create(JobOfferDTO dto)
        {
            dto.DateCreated = DateTime.Now;
            JobOffer jobOffer = ConvertFromDTO(dto);

            _jobOfferRepository.Create(jobOffer);
            _jobOfferRepository.Save();

            if(jobOffer.Id != 0)
            {
                try
                {
                    await SaveJobOffersImages(dto.Files, jobOffer.Id);
                }
                catch(Exception e)
                {

                }

            }

            return ConvertToDTO(jobOffer);
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

        public async Task<string> TestAsyncMethod(string fileName)
        {
            string result = fileName;

            await Task.Run(() => {
                Thread.Sleep(2000);
                result += " executed! ";
            });

            result += " afterExec ";
            return result;
        }

        public async Task<string> TestAsyncMethod2(string fileName)
        {
            string result = fileName;

            await Task.Run(() => {
                Thread.Sleep(4000);
                result += " executed! ";
            });

            result += " afterExec ";
            return result;
        }

        public async Task<string> TestAsyncMethod3(string fileName)
        {
            string result = fileName;

            await Task.Run(() => {
                Thread.Sleep(7000);
                result += " executed! ";
            });

            result += " afterExec ";
            return result;
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

        
    }
}
