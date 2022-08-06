using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    public interface IJobOfferRepository: IGenericRepository<JobOffer>
    {
        JobOffer GetById(int Id);
        Task AddSkillsOnThisJobOfferAsync(int id, List<int> IdSkills);
        Task<List<JobOffer>> GetAllJobOffersInclude(int skip, int noElements, FilterJobOffersDTO filter);
        Task<int> GetTotalNrOfJobOffers();
        Task<List<CityShortDTO>> GetAllCities();
    }
}
