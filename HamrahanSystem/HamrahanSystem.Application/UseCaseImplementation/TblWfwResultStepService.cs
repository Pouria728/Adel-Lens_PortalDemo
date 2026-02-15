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
    public class TblWfwResultStepService(ITblWfwResultStepRepository TblWfwResultStepRepository) :ITblWfwResultStepService
    {
        public Task Add(TblWfwResultStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwResultStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwResultStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwResultStepDto>> GetAll()
        {
            var TblWfwResultSteps= TblWfwResultStepRepository.GetAll();
            
            return TblWfwResultStepConverter.ToDtos(TblWfwResultSteps);

        }

        public async Task<TblWfwResultStepDto> GetById(int id)
        {
            var TblWfwResultStep = TblWfwResultStepRepository.GetByKey(id);
            return TblWfwResultStepConverter.ToDto(TblWfwResultStep);
        }

        public Task Update(TblWfwResultStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwResultStepDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
