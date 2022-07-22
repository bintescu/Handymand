using Handymand.Data;
using Handymand.Models;
using Handymand.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository (HandymandContext context) : base(context)
        {

        }

        public List<User> GetAllWithInclude()
        {
            return _table.Include(x => x.FreelancerAccount)
                .Include(x => x.ClientAccount)
                .ToList();
        }

        public async Task<User> GetById(int Id)
        {
            return await _table.Where(i => i.Id == Id).FirstOrDefaultAsync();
        }

        public User GetByIdIncludingAll(int Id)
        {
            return _table.Where(i => i.Id == Id)
                .Include(x => x.ClientAccount)
                .Include(x => x.FreelancerAccount).FirstOrDefault();

        }

        public async Task<User> GetByEmail(string email)
        {
            return await _table.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
        }
    }
}
