using Handymand.Data;
using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    public class JobOfferRepository : GenericRepository<JobOffer>, IJobOfferRepository
    {
        private HandymandContext _context;

        public JobOfferRepository(HandymandContext context) : base(context)
        {
            _context = context;
        }


        public async Task<JobOffer> GetById(int Id)
        {

            var result = await _table.Where(j => j.Id == Id).Include("JobOffersSkills.Skill")
                                     .Include("CreationUser")
                                     .Include("City")
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync();


            return result;
        }

        public async Task AddSkillsOnThisJobOfferAsync(int id, List<int> IdSkills)
        {
            await Task.Run(() =>
            {
                var existingSkills = _context.Skills.Where(s => IdSkills.Contains(s.Id)).ToList();


                foreach (var s in existingSkills)
                {
                    JobOffersSkills jos = new JobOffersSkills();
                    jos.IdJobOffer = id;
                    jos.IdSkill = s.Id;
                    _context.Add<JobOffersSkills>(jos);
                }
            });


            await _context.SaveChangesAsync();

        }

        private bool VerifyNames(string firstName, string lastName, string[] nameSplitted)
        {
            string[] user_fullname = new string[] { firstName.ToLower(), lastName.ToLower() };

            string[] lowerNameSplitted = new string[nameSplitted.Length];

            for(int i = 0; i<nameSplitted.Length; i++)
            {
                lowerNameSplitted[i] = nameSplitted[i].ToLower();
            }


            var intersect = user_fullname.Intersect(lowerNameSplitted);

            if (intersect.Count() == nameSplitted.Length)
            {
                return true;
            }

            return false;

        }


        private bool VerifyKeywords(string description, string[] keywords)
        {
            string[] allDescriptionLower = description.ToLower().Split(' ', '.', '!', ':', ',');

            string[] lowerKeywords = new string[keywords.Length];

            for (int i = 0; i < keywords.Length; i++)
            {
                lowerKeywords[i] = keywords[i].ToLower().Trim();
            }

            return allDescriptionLower.Intersect(lowerKeywords).Any();

        }

        public async Task<List<JobOffer>> GetAllJobOffersInclude(int skip, int noElements, FilterJobOffersDTO filter)
        {

            var filteredList = await _table.Include("JobOffersSkills.Skill")
                                     .Include("CreationUser")
                                     .Include("City")
                                     .AsNoTracking()
                                     .ToListAsync();


            await Task.Run(() =>
            {
                string CreatorName = filter.CreatorName;

                if (CreatorName != "" && CreatorName != null)
                {
                    string[] inputNamesSplitted = filter.CreatorName.TrimStart().TrimEnd().Split(" ");
                    filteredList = filteredList.Where(j => VerifyNames(j.CreationUser.LastName, j.CreationUser.FirstName, inputNamesSplitted)).ToList();
                }


                int? CityId = filter.CityId;
                if (CityId != null && CityId != 0)
                {
                    filteredList = filteredList.Where(j => j.CityId == CityId).ToList();
                }


                string fKeywords = filter.Keywords;

                if (fKeywords != null && fKeywords != "" && fKeywords.Length > 2)
                {
                    var keywords = fKeywords.Split(" ");
                    filteredList = filteredList.Where(j => VerifyKeywords(j.Description, keywords)).ToList();
                }


                List<SkillShortDTO> fSkills = (List<SkillShortDTO>)filter.Skills;

                if (fSkills != null && fSkills.Count() > 0)
                {
                    int[] ids = new int[fSkills.Count()];

                    for (int i = 0; i < fSkills.Count(); i++)
                    {
                        ids[i] = fSkills[i].Id.Value;
                    }


                    var jobOffersIdsForGivenSkills = _context.JobOffersSkills.Where(jos => ids.Contains(jos.IdSkill)).Select(jos => jos.IdJobOffer).ToArray();

                    filteredList = filteredList.Where(j => jobOffersIdsForGivenSkills.Contains(j.Id)).ToList();
                }


                int? lowPriceRange = filter.lowPriceRange;
                if (lowPriceRange.HasValue && lowPriceRange != null)
                {
                    filteredList = filteredList.Where(j => j.LowPriceRange >= lowPriceRange).ToList();
                }

                int? highPriceRange = filter.highPriceRange;
                if (highPriceRange.HasValue && highPriceRange != null)
                {
                    filteredList = filteredList.Where(j => j.HighPriceRange <= highPriceRange).ToList();
                }



            });

            if (skip < 0 || noElements == 0)
            {
                return filteredList.ToList();
            }
            else
            {
                return filteredList.Skip(skip).Take(noElements).ToList();
            }
        }

        public async Task<int> GetTotalNrOfJobOffers()
        {
            return await _table.CountAsync();
        }

        public async Task<List<CityShortDTO>> GetAllCities()
        {
            return await _context.Cities.Select(c => new CityShortDTO()
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();
        }
    }
}
