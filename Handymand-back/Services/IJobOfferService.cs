using Handymand.Models;
using Handymand.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public interface IJobOfferService
    {
        public List<JobOfferDTO> AllJobOffers();

        public JobOfferDTO Create(JobOfferDTO dto);

        public JobOfferDTO GetById(int Id);

    }
}
