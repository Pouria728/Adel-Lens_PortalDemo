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
    public class TblWfwUserCartableService(ITblWfwUserCartableRepository TblWfwUserCartableRepository) :ITblWfwUserCartableService
    {
        public Task Add(TblWfwUserCartableDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwUserCartableDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwUserCartableDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwUserCartableDto>> GetAll()
        {
            var TblWfwUserCartables= TblWfwUserCartableRepository.GetAll();
            
            return TblWfwUserCartableConverter.ToDtos(TblWfwUserCartables);

        }

        public async Task<TblWfwUserCartableDto> GetById(int id)
        {
            var TblWfwUserCartable = TblWfwUserCartableRepository.GetByKey(id);
            return TblWfwUserCartableConverter.ToDto(TblWfwUserCartable);
        }

        public Task Update(TblWfwUserCartableDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwUserCartableDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
