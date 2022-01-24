using Handymand.Models;
using Handymand.Models.DTOs;
using Handymand.Repository.DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handymand.Services
{
    public class SkillService: ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public SkillDTO ConvertToDTO(Skill skill)
        {
            SkillDTO dto = new SkillDTO();
            dto.SkillName = skill.SkillName;
            dto.Id = skill.Id;

            return dto;
        }

        public Skill ConvertFromDTOForCreate(SkillDTO skill)
        {
            Skill item = new Skill();
            item.SkillName = skill.SkillName;
            item.DateCreated = DateTime.Now;

            return item;
        }

        public SkillDTO Create(SkillDTO skill)
        {
            var forCreate = ConvertFromDTOForCreate(skill);

            _skillRepository.Create(forCreate);
            _skillRepository.Save();

            return ConvertToDTO(forCreate);
        }

        public SkillDTO Update(SkillDTO skill) 
        {
            if(skill.Id != null && skill.Id != 0)
            {
                var forUpdate = _skillRepository.FindById((int)skill.Id);

                if (forUpdate != null)
                {

                    forUpdate.SkillName = skill.SkillName;
                    forUpdate.DateModified = DateTime.Now;

                    _skillRepository.Update(forUpdate);
                    _skillRepository.Save();

                    return ConvertToDTO(forUpdate);
                }
                else
                {
                    throw new Exception("UPDATE : There is no skill with Id :" + skill.Id);
                }
            }
            else
            {
                throw new Exception("UPDATE: The received skill for update have null Id");
            }

        }

        public SkillDTO Delete(SkillDTO skill)
        {
            if(skill.Id != null && skill.Id != 0)
            {
                var forDelete = _skillRepository.FindById((int)skill.Id);

                if(forDelete != null)
                {
                    _skillRepository.Delete(forDelete);
                    _skillRepository.Save();

                    return ConvertToDTO(forDelete);
                }
                else
                {
                    throw new Exception("DELETE : There is no skill with Id :" + skill.Id);
                }

            }
            else
            {
                throw new Exception("DELETE : The received skill for delete have null Id");
            }

        }


        public IEnumerable<SkillDTO> GetAll()
        {
            var query = _skillRepository.GetAllAsQueryable().ToList();

            var result = new List<SkillDTO>();

            foreach(var elem in query)
            {
                result.Add(ConvertToDTO(elem));

            }

            return result;
        }


    }
}
