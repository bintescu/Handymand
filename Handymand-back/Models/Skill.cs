using Handymand.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Models
{
    public class Skill:BaseEntity
    {
        public string SkillName { get; set; }
        
        public ICollection<FreelancersSkills> FreelancersSkills { get; set; }

        public ICollection<ContractsSkills> ContractSkills { get; set; }
    }
}
