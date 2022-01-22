using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public class ContractService : IContractService
    {
        private IContractRepository _contractRepository;
        private IUserRepository _userRepository;
        private IContractsSkillsRepository _contractsSkillsRepository;

        public ContractService(IContractRepository contractRepository, IUserRepository userRepository, IContractsSkillsRepository contractsSkillsRepository)
        {
            _contractRepository = contractRepository;
            _userRepository = userRepository;
            _contractsSkillsRepository = contractsSkillsRepository;
        }

        public ContractDTO ConvertToDTO(Contract contract)
        {
            var DTO = new ContractDTO();

            DTO.Id = contract.Id;
            DTO.IdCreationUser = contract.IdCreationUser;
            var creationUser = _userRepository.FindById(contract.IdCreationUser);
            if (creationUser != null)
            {
                DTO.CreationUserFullName = creationUser.LastName + " " + creationUser.FirstName;
            }

            if (contract.IdRefferedUser != null && contract.IdRefferedUser != 0)
            {
                DTO.IdRefferedUser = contract.IdRefferedUser;

                var refferedUser = _userRepository.FindById((int)contract.IdRefferedUser);

                DTO.RefferedUserFullName = refferedUser.LastName + " " + refferedUser.FirstName;

            }
            else
            {
                DTO.ExpirationDate = contract.DateCreated.AddMonths(1);
            }

            DTO.CreationDate = contract.DateCreated;

            DTO.ExpectedDurationInHours = contract.ExpectedDurationInHours;
            DTO.PaymentAmount = contract.PaymentAmount;
            DTO.ComplexityGrade = contract.ComplexityGrade;

            return DTO;


        }

        public Contract ConvertFromDTOforCreate(ContractDTO contract)
        {
            var result = new Contract();

            result.IdCreationUser = contract.IdCreationUser;
            result.IdRefferedUser = contract.IdRefferedUser;
            result.PaymentAmount = contract.PaymentAmount;
            result.ExpectedDurationInHours = contract.ExpectedDurationInHours;
            result.Description = contract.Description;
            result.DateCreated = DateTime.Now;
            result.ComplexityGrade = contract.ComplexityGrade;

            return result;
        }

        public List<ContractDTO> AllAvailableContracts()
        {
            var query = _contractRepository.GetAllWithInclude();

            var result = query.Where(i => i.IdRefferedUser == null || i.IdRefferedUser == 0)
                .Select(i => new ContractDTO()
                {
                    Id = i.Id,
                    CreationUserFullName = i.CreationUser.LastName + " " + i.CreationUser.FirstName,
                    Description = i.Description,
                    CreationDate = i.DateCreated,
                    ExpirationDate = i.DateCreated.AddMonths(1),
                    ComplexityGrade = i.ComplexityGrade,
                    PaymentAmount = i.PaymentAmount,
                    ExpectedDurationInHours = i.ExpectedDurationInHours
                }).ToList();

            return result;
        }

        public ContractDTO UpdateContract(ContractDTO contract)
        {
            var forupdate = _contractRepository.GetById(contract.Id);

            if(forupdate != null)
            {
                forupdate.IdRefferedUser = contract.IdRefferedUser;
                forupdate.PaymentAmount = contract.PaymentAmount;
                forupdate.ExpectedDurationInHours = contract.ExpectedDurationInHours;
                forupdate.ComplexityGrade = contract.ComplexityGrade;

                _contractRepository.Update(forupdate);
                _contractRepository.Save();

                return ConvertToDTO(forupdate);
            }
            else
            {

                throw new Exception("There is no contract with id : " + contract.Id);
            }
        }

        public ContractDTO CreateContract(ContractDTO contract)
        {
            var forCreate = ConvertFromDTOforCreate(contract);

            _contractRepository.Create(forCreate);

            _contractRepository.Save();

            if(contract.SkillsList != null && contract.SkillsList.Count() > 0)
            foreach(var skillId in contract.SkillsList)
            {
                ContractsSkills Cskill = new ContractsSkills();
                Cskill.IdContract = contract.Id;
                Cskill.IdSkill = skillId;

                _contractsSkillsRepository.Create(Cskill);
                _contractsSkillsRepository.Save();

            }

            _contractRepository.Save();

            return ConvertToDTO(forCreate);
        }

        public bool DeleteContract(ContractDTO contract)
        {
            var forDelete = _contractRepository.FindById(contract.Id);

            if(forDelete != null)
            {
                _contractRepository.Delete(forDelete);
                _contractRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
