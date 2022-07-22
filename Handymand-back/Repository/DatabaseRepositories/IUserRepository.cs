using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetById(int Id);

        User GetByIdIncludingAll(int Id);

        List<User> GetAllWithInclude();

        Task<User> GetByEmail(string email);

       // Task<User> UpdateUser(User userForUpdate, User newVariant);
    }
}
