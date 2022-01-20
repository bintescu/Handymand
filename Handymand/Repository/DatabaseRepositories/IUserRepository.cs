using Handymand.Models;
using Handymand.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Repository.DatabaseRepositories
{
    interface IUserRepository: IGenericRepository<User>
    {
        User GetById(int Id);

        User GetByIdIncludingAll(int Id);

        List<User> GetAllWithInclude();
    }
}
