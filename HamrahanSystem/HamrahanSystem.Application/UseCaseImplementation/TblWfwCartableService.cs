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
    public class TblWfwCartableService(ITblWfwCartableRepository TblWfwCartableRepository) :ITblWfwCartableService
    {
        public Task Add(TblWfwCartableDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwCartableDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwCartableDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwCartableDto>> GetAll()
        {
            var TblWfwCartables= TblWfwCartableRepository.GetAll();
            
            return TblWfwCartableConverter.ToDtos(TblWfwCartables);

        }

        public async Task<TblWfwCartableDto> GetById(int id)
        {
            var TblWfwCartable = TblWfwCartableRepository.GetByKey(id);
            return TblWfwCartableConverter.ToDto(TblWfwCartable);
        }

        public Task Update(TblWfwCartableDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwCartableDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
