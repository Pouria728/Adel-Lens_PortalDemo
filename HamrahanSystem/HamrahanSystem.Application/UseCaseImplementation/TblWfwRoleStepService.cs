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
    public class TblWfwRoleStepService(ITblWfwRoleStepRepository TblWfwRoleStepRepository) :ITblWfwRoleStepService
    {
        public Task Add(TblWfwRoleStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwRoleStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwRoleStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwRoleStepDto>> GetAll()
        {
            var TblWfwRoleSteps= TblWfwRoleStepRepository.GetAll();
            
            return TblWfwRoleStepConverter.ToDtos(TblWfwRoleSteps);

        }

        public async Task<TblWfwRoleStepDto> GetById(int id)
        {
            var TblWfwRoleStep = TblWfwRoleStepRepository.GetByKey(id);
            return TblWfwRoleStepConverter.ToDto(TblWfwRoleStep);
        }

        public Task Update(TblWfwRoleStepDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwRoleStepDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
