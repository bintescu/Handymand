using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public class ContractService: IContractService
    {
        public IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public List<ContractDTO> AllAvailableContracts()
        {
            var query = _contractRepository.GetAllWithInclude();

            var result = query.Where(i => i.IdRefferedUser == null || i.IdRefferedUser == 0)
                .Select(i => new ContractDTO()
                {
                    Id = i.Id,
                    CreationUserFullName = i.CreationUser.User.LastName + " " + i.CreationUser.User.FirstName,
                    Description = i.Description,
                    CreationDate = i.DateCreated,
                    ExpirationDate = i.DateCreated.HasValue ? i.DateCreated.Value.Date.AddMonths(1) : null,
                    ComplexityGrade = i.ComplexityGrade,
                    PaymentAmount = i.PaymentAmount,
                    ExpectedDurationInHours = i.ExpectedDurationInHours
                }).ToList();

            return result;
        }
    }
}
