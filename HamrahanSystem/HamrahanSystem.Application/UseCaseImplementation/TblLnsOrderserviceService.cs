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
    public class TblLnsOrderserviceService(ITblLnsOrderserviceRepository TblLnsOrderserviceRepository) :ITblLnsOrderserviceService
    {
        public Task Add(TblLnsOrderserviceDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblLnsOrderserviceDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsOrderserviceDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsOrderserviceDto>> GetAll()
        {
            var TblLnsOrderservices=TblLnsOrderserviceRepository.GetAll();
            
            return TblLnsOrderserviceConverter.ToDtos(TblLnsOrderservices);

        }

        public async Task<TblLnsOrderserviceDto> GetById(int id)
        {
            var TblLnsOrderservice =TblLnsOrderserviceRepository.GetByKey(id);
            return TblLnsOrderserviceConverter.ToDto(TblLnsOrderservice);
        }

        public Task Update(TblLnsOrderserviceDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsOrderserviceDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
