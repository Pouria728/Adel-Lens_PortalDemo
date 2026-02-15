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
    public class TblDefineServiceService(ITblDefineServiceRepository TblDefineServiceRepository) :ITblDefineServiceService
    {
        public Task Add(TblDefineServiceDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblDefineServiceDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblDefineServiceDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblDefineServiceDto>> GetAll()
        {
            var TblDefineServices= TblDefineServiceRepository.GetAll();
            
            return TblDefineServiceConverter.ToDtos(TblDefineServices);

        }

        public async Task<TblDefineServiceDto> GetById(int id)
        {
            var TblDefineService = TblDefineServiceRepository.GetByKey(0, id);
            return TblDefineServiceConverter.ToDto(TblDefineService);
        }

        public Task Update(TblDefineServiceDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblDefineServiceDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
