using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    public interface ISkillRepository: IGenericRepository<Skill>
    {
        Task<Skill> FindByNameAsync(string name);

        Task<List<Skill>> GetAllSkillsIncludeAsync();
        Task<List<SkillShortDTO>> GetSkillShortDTOsAsync();
    }
}
