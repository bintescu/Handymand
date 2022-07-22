using Handymand.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Handymand.Models
{
    public class User: BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WalletAddress { get; set; }
        public decimal Amount { get; set; } 
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public DateTime? Birthday { get; set; }

        public Client ClientAccount { get; set; }
        public Freelancer FreelancerAccount { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public Role Role { get; set; }
        //public ICollection<Feedback> SentFeedbacks { get; set; }
        public ICollection<Contract> CreatedContracts { get; set; }

        public ICollection<JobOffer> CreatedJobOffers { get; set; }

    }
}
