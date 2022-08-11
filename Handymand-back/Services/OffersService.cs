using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{

    public class OffersService: IOffersService
    {
        private readonly IOffersRepository _offersRepository;

        public OffersService(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }

        private OfferGetDTO ConvertToDTO(Offer jb)
        {
            OfferGetDTO dto = new OfferGetDTO();
            dto.Id = jb.Id;
            if(jb.CreationUser != null)
            {
                if(jb.CreationUser.Birthday != null)
                {
                    dto.CreationUserAge = DateTime.Now.Year - jb.CreationUser.Birthday.Value.Year;
                }
                dto.CreationUserId = (int)jb.CreationUserId;
                dto.CreationUserName = jb.CreationUser.LastName + " " + jb.CreationUser.FirstName;
                dto.CreationUserTitle = jb.CreationUser.Title;
                //Poza de profil v-a fi adusa pentru fiecare user in parte cu un query specific

                

            }
            dto.Description = jb.Description;
            dto.PaymentAmount = jb.PaymentAmount;
            dto.DateCreated = jb.DateCreated;
            dto.Available = jb.Available;

            return dto;
        }

        private Offer ConvertFromDTOForCreate(OfferCreateDTO dto)
        {
            Offer offer = new Offer();

            offer.DateCreated = DateTime.Now;
            offer.Description = dto.Description;
            offer.JobOfferId = dto.JobOfferId;
            offer.PaymentAmount = dto.PaymentAmount;
            offer.CreationUserId = dto.CreationUserId;
            offer.Available = true;

            return offer;
        }


        private OffersForLoggedInDTO ConvertToDTOforLoggedIn(Offer offer)
        {
            OffersForLoggedInDTO dto = new OffersForLoggedInDTO();
            dto.Id = offer.Id;
            dto.JobOfferId = offer.JobOfferId;
            dto.JobOfferTitle = offer.JobOffer != null ? offer.JobOffer.Title : null;
            dto.PaymentAmount = offer.PaymentAmount;
            dto.DateCreated = offer.DateCreated;

            return dto;
        }
        public async Task<List<OffersForLoggedInDTO>> GetAllOffersForLoggedIn(int id)
        {
            var list = await _offersRepository.GetAllOffersForLoggedIn(id);

            return list.Select(offer => ConvertToDTOforLoggedIn(offer)).ToList();

        }


        public async Task<List<OfferGetDTO>> GetAllOffers(int id,int pageNr, int noElements, int sortOption)
        {

            int skip = (pageNr) * noElements;


            var query = await _offersRepository.GetAllOffersIncludeUsers(id, skip, noElements,sortOption);

            var result = new List<OfferGetDTO>();

            await Task.Run(() =>
            {
                foreach (Offer offer in query)
                {
                    var dto = ConvertToDTO(offer);
                    result.Add(dto);

                }
            });

           return result;
        }


        public async Task<int> GetTotalNrOfJobOffers(int id)
        {
            return await _offersRepository.GetTotalNrOfJobOffers(id);
        }


        public async Task<bool> Create(OfferCreateDTO offerdto)
        {
            var alreadyExist = await _offersRepository.FinByIdUserAndIdJobOffer(offerdto);

            if (alreadyExist != null)
            {
                throw new Exception("You already have an offer on this job offer !");
            }

            var sameUser = await _offersRepository.JobOfferCreationUserTryToCreateOffer(offerdto);

            if (sameUser)
            {
                throw new Exception("You can not create offer to your job offer!");
            }


            var forCreate = ConvertFromDTOForCreate(offerdto);

            await _offersRepository.CreateAsync(forCreate);
            return await _offersRepository.SaveAsync();


        }

        public async Task<bool> AcceptOffer(AcceptOfferDTO dto, int loggedInId)
        {
            return await _offersRepository.AcceptOffer(dto, loggedInId);
        }

        public async Task<List<OffersForLoggedInDTO>> GetAllMyAcceptedOffersOrderByDateCreated(int id)
        {
            var list = await _offersRepository.GetAllMyAcceptedOffersOrderByDateCreated(id);

            var result = list.Where(j => j.JobOffer != null && 
                                    j.JobOffer.Available == false &&
                                    j.JobOffer.Contract.Valid == true)
                            .Select(j => new OffersForLoggedInDTO() 
                            {
                                Id = j.Id,
                                DateCreated = j.DateCreated,
                                JobOfferId = j.JobOfferId,
                                JobOfferTitle =j.JobOffer.Title,
                                PaymentAmount = j.PaymentAmount
                            }).ToList();

            return result;

        }

        public async Task<RatingDTO> GetRatingForFreelancer(int userId)
        {
            return await _offersRepository.GetRatingForFreelancer(userId);
        }

        public async Task<RatingDTO> GetRatingForCustomer(int userId)
        {
            return await _offersRepository.GetRatingForCustomer(userId);
        }
    }
}
