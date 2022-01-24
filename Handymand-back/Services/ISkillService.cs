using Handymand.Models;
using Handymand.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface ISkillService
    {
        SkillDTO ConvertToDTO(Skill skill);
        Skill ConvertFromDTOForCreate(SkillDTO skill);

        SkillDTO Create(SkillDTO skill);

        IEnumerable<SkillDTO> GetAll();
        SkillDTO Update(SkillDTO skill);
        SkillDTO Delete(SkillDTO skill);
    }
}
