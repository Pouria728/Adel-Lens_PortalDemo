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
    public class TblLnsBrandCoatingService(ITblLnsBrandCoatingRepository TblLnsBrandCoatingRepository) :ITblLnsBrandCoatingService
    {
        public Task Add(TblLnsBrandCoatingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblLnsBrandCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsBrandCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsBrandCoatingDto>> GetAll()
        {
            var TblLnsBrandCoatings= TblLnsBrandCoatingRepository.GetAll();
            
            return TblLnsBrandCoatingConverter.ToDtos(TblLnsBrandCoatings);

        }

        public async Task<TblLnsBrandCoatingDto> GetById(int id)
        {
            var TblLnsBrandCoating =TblLnsBrandCoatingRepository.GetByKey(id);
            return TblLnsBrandCoatingConverter.ToDto(TblLnsBrandCoating);
        }

        public Task Update(TblLnsBrandCoatingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsBrandCoatingDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
