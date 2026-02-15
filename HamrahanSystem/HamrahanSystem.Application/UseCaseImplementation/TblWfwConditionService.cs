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
    public class TblWfwConditionService(ITblWfwConditionRepository TblWfwConditionRepository) :ITblWfwConditionService
    {
        public Task Add(TblWfwConditionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwConditionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwConditionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwConditionDto>> GetAll()
        {
            var TblWfwConditions= TblWfwConditionRepository.GetAll();
            
            return TblWfwConditionConverter.ToDtos(TblWfwConditions);

        }

        public async Task<TblWfwConditionDto> GetById(int id)
        {
            var TblWfwCondition = TblWfwConditionRepository.GetByKey(id);
            return TblWfwConditionConverter.ToDto(TblWfwCondition);
        }

        public Task Update(TblWfwConditionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwConditionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
