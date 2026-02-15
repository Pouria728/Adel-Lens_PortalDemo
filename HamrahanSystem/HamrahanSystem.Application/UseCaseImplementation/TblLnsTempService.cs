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
    public class TblLnsTempService(ITblLnsTempRepository TblLnsTempRepository) :ITblLnsTempService
    {
        public Task Add(TblLnsTempDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblLnsTempDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsTempDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsTempDto>> GetAll()
        {
            var TblLnsTemps=TblLnsTempRepository.GetAll();
            
            return TblLnsTempConverter.ToDtos(TblLnsTemps);

        }

        public async Task<TblLnsTempDto> GetById(int id)
        {
            var TblLnsTemp =TblLnsTempRepository.GetByKey(id);
            return TblLnsTempConverter.ToDto(TblLnsTemp);
        }

        public Task Update(TblLnsTempDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsTempDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
