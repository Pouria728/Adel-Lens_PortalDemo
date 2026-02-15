using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomLensTypeCoatingService(ITblLnsCustomLensTypeCoatingRepository TblLnsCustomLensTypeCoatingRepository) : ITblLnsCustomLensTypeCoatingService
    {
        public Task Add(TblLnsCustomLensTypeCoatingDto dto)
        {
            var item = dto.ToEntity();
            return TblLnsCustomLensTypeCoatingRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomLensTypeCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsCustomLensTypeCoatingRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomLensTypeCoatingDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsCustomLensTypeCoatingDto>> GetAll()
        {
            var items = TblLnsCustomLensTypeCoatingRepository.GetAll();
            return TblLnsCustomLensTypeCoatingConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomLensTypeCoatingDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
        {
            var result = TblLnsCustomLensTypeCoatingRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomLensTypeCoatingConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomLensTypeCoatingDto> GetById(int id)
        {
            var item = TblLnsCustomLensTypeCoatingRepository.GetByKey(id);
            return TblLnsCustomLensTypeCoatingConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomLensTypeCoatingDto dto)
        {
            var item = dto.ToEntity();
            return TblLnsCustomLensTypeCoatingRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomLensTypeCoatingDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}

