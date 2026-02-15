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
    public class TblWfwAttachService(ITblWfwAttachRepository TblWfwAttachRepository) :ITblWfwAttachService
    {
        public Task Add(TblWfwAttachDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblWfwAttachDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwAttachDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwAttachDto>> GetAll()
        {
            var TblWfwAttachs=TblWfwAttachRepository.GetAll();
            
            return TblWfwAttachConverter.ToDtos(TblWfwAttachs);

        }

        public async Task<TblWfwAttachDto> GetById(int id)
        {
            var TblWfwAttach =TblWfwAttachRepository.GetByKey(id);
            return TblWfwAttachConverter.ToDto(TblWfwAttach);
        }

        public Task Update(TblWfwAttachDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblWfwAttachDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
