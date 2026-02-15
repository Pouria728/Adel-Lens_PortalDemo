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
    public class TblWfwRelationStepService(ITblWfwRelationStepRepository TblWfwRelationStepRepository) :ITblWfwRelationStepService
    {
        public Task Add(TblWfwRelationStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwRelationStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwRelationStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwRelationStepDto>> GetAll()
        {
            var TblWfwRelationSteps= TblWfwRelationStepRepository.GetAll();
            
            return TblWfwRelationStepConverter.ToDtos(TblWfwRelationSteps);

        }

        public async Task<TblWfwRelationStepDto> GetById(int id)
        {
            var TblWfwRelationStep = TblWfwRelationStepRepository.GetByKey(id);
            return TblWfwRelationStepConverter.ToDto(TblWfwRelationStep);
        }

        public Task Update(TblWfwRelationStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwRelationStepDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
