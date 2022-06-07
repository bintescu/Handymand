using Handymand.Data;
using Handymand.Models;
using Handymand.Repository.GenericRepository;
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
        public JobOffer GetById(int Id)
        {
            var result = _table.Where(j => j.Id == Id).FirstOrDefault();

            if(result.CreationUserId != null)
            {
                result.CreationUser = _context.Users.Where(i => i.Id == result.CreationUserId).FirstOrDefault();
            }


            return result;
        }
    }
}
