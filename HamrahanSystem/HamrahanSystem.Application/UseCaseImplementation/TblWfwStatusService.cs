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
    public class TblWfwStatusService(ITblWfwStatusRepository TblWfwStatusRepository) :ITblWfwStatusService
    {
        public Task Add(TblWfwStatusDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwStatusDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwStatusDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwStatusDto>> GetAll()
        {
            var TblWfwStatuss= TblWfwStatusRepository.GetAll();
            
            return TblWfwStatusConverter.ToDtos(TblWfwStatuss);

        }

        public async Task<TblWfwStatusDto> GetById(int id)
        {
            var TblWfwStatus = TblWfwStatusRepository.GetByKey(id);
            return TblWfwStatusConverter.ToDto(TblWfwStatus);
        }

        public Task Update(TblWfwStatusDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwStatusDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
