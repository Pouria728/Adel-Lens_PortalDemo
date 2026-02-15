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
    public class TblLnsCoatingService(ITblLnsCoatingRepository TblLnsCoatingRepository) : ITblLnsCoatingService
    {
        public Task Add(TblLnsCoatingDto dto)
        {
            return TblLnsCoatingRepository.Add(dto.ToEntity());
        }

        public Task Add(List<TblLnsCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsCoatingDto>> GetAll()
        {
            var TblLnsCoatings = TblLnsCoatingRepository.GetAll();

            return TblLnsCoatingConverter.ToDtos(TblLnsCoatings);

        }
        public Task<(List<TblLnsCoatingDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var tblLnsCoatings = TblLnsCoatingRepository.GetAll(maxResult, page, rowInPage, sort, sidx);

            return Task.FromResult((TblLnsCoatingConverter.ToDtos(tblLnsCoatings.Result.Item1), tblLnsCoatings.Result.Item2));

        }

        public async Task<TblLnsCoatingDto> GetById(int id)
        {
            var TblLnsCoating = TblLnsCoatingRepository.GetByKey(id);
            return TblLnsCoatingConverter.ToDto(TblLnsCoating);
        }

        public Task Update(TblLnsCoatingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsCoatingDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
