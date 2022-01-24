using Handymand.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Models
{
    public class Contract: BaseEntity
    {
        public int IdCreationUser { get; set;}
        public User CreationUser { get; set;}

        public int? IdRefferedUser { get; set; }

        public int ExpectedDurationInHours { get; set; }

        public string Description { get; set; }

        public ICollection<ContractsSkills> ContractSkills { get; set; }

        public decimal PaymentAmount { get; set; }

        public int ComplexityGrade { get; set; }


    }
}
