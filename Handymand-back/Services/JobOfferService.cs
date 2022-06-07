using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public JobOfferDTO Create(JobOfferDTO dto)
        {
            dto.DateCreated = DateTime.Now;
            JobOffer jobOffer = ConvertFromDTO(dto);

            _jobOfferRepository.Create(jobOffer);
            _jobOfferRepository.Save();

            return ConvertToDTO(jobOffer);
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
