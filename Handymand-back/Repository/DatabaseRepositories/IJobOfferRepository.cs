using Handymand.Models;
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
        

    }
}
