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
    public class TblWfwProcessActionService(ITblWfwProcessActionRepository TblWfwProcessActionRepository) :ITblWfwProcessActionService
    {
        public Task Add(TblWfwProcessActionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwProcessActionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwProcessActionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwProcessActionDto>> GetAll()
        {
            var TblWfwProcessActions= TblWfwProcessActionRepository.GetAll();
            
            return TblWfwProcessActionConverter.ToDtos(TblWfwProcessActions);

        }

        public async Task<TblWfwProcessActionDto> GetById(int id)
        {
            var TblWfwProcessAction = TblWfwProcessActionRepository.GetByKey(id);
            return TblWfwProcessActionConverter.ToDto(TblWfwProcessAction);
        }

        public Task Update(TblWfwProcessActionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwProcessActionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
