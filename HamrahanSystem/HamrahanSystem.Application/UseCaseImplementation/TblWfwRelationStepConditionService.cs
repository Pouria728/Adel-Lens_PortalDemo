using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblWfwRelationStepConditionService(ITblWfwRelationStepConditionRepository TblWfwRelationStepConditionRepository) :ITblWfwRelationStepConditionService
    {
        public Task Add(TblWfwRelationStepConditionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwRelationStepConditionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwRelationStepConditionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwRelationStepConditionDto>> GetAll()
        {
            var TblWfwRelationStepConditions= TblWfwRelationStepConditionRepository.GetAll();
            
            return TblWfwRelationStepConditionConverter.ToDtos(TblWfwRelationStepConditions);

        }

        public async Task<TblWfwRelationStepConditionDto> GetById(int id)
        {
            var TblWfwRelationStepCondition = TblWfwRelationStepConditionRepository.GetByKey(id);
            return TblWfwRelationStepConditionConverter.ToDto(TblWfwRelationStepCondition);
        }

        public Task Update(TblWfwRelationStepConditionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwRelationStepConditionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
