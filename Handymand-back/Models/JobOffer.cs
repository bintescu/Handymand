using Handymand.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Models
{
    public class JobOffer: BaseEntity
    {
        public User CreationUser { get; set; }
        public int? CreationUserId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double LowPriceRange { get; set; }
        public double HighPriceRange { get; set; }
        public string Title { get; set; }
    }
}
