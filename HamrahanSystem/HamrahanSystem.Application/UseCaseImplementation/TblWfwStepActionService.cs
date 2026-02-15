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
    public class TblWfwStepActionService(ITblWfwStepActionRepository TblWfwStepActionRepository) :ITblWfwStepActionService
    {
        public Task Add(TblWfwStepActionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwStepActionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwStepActionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwStepActionDto>> GetAll()
        {
            var TblWfwStepActions= TblWfwStepActionRepository.GetAll();
            
            return TblWfwStepActionConverter.ToDtos(TblWfwStepActions);

        }

        public async Task<TblWfwStepActionDto> GetById(int id)
        {
            var TblWfwStepAction = TblWfwStepActionRepository.GetByKey(id);
            return TblWfwStepActionConverter.ToDto(TblWfwStepAction);
        }

        public Task Update(TblWfwStepActionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwStepActionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
