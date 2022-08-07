using Handymand.Models;
using Handymand.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface IJobOfferService
    {
        Task<List<JobOfferDTO>> GetAllJobOffers(int pageNr, int noElements, FilterJobOffersDTO filter);

        Task<ServiceResponse<JobOfferDTO>> Create(JobOfferDTO dto);

        Task<ServiceResponse<JobOfferDTO>> GetById(int Id);

        Task SaveJobOffersImages(List<IFormFile> files, int N);
        Task<int> GetTotalNrOfJobOffers();

        Task<List<CityShortDTO>> GetAllCities();
        Task<List<byte[]>> GetImages(int id);
    }
}
