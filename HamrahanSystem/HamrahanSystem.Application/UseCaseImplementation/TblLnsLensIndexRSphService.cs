using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsLensIndexRSphService(ITblLnsLensIndexRSphRepository TblLnsLensIndexRSphRepository,ICacheService cacheService) :ITblLnsLensIndexRSphService
    {
        public Task Add(TblLnsLensIndexRSphDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsLensIndexRSphDto");
			return TblLnsLensIndexRSphRepository.Add(item);
		}

        public Task Add(List<TblLnsLensIndexRSphDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsLensIndexRSphRepository.Delete(id);
        }

        public Task Delete(List<TblLnsLensIndexRSphDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblLnsLensIndexRSphDto>> GetAll()
        {
			var cache = cacheService.GetData<List<TblLnsLensIndexRSphDto>>("TblLnsLensIndexRSphDto");
            if (cache == null)
            {
                var TblLnsLensIndexRSphs = TblLnsLensIndexRSphRepository.GetAll().ToList();
                var items = TblLnsLensIndexRSphConverter.ToDtosWithRelated(TblLnsLensIndexRSphs, 1);
                cacheService.SetData<List<TblLnsLensIndexRSphDto>>("TblLnsLensIndexRSphDto", items);
                return items;
            }
            else
                return cache;
        }
		public Task<(List<TblLnsLensIndexRSphDto>, int)> GetAll(int lensTypeRLensIndexRId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsLensIndexRSphs = TblLnsLensIndexRSphRepository.GetAll(lensTypeRLensIndexRId, maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((TblLnsLensIndexRSphConverter.ToDtosWithRelated(TblLnsLensIndexRSphs.Result.Item1,3), TblLnsLensIndexRSphs.Result.Item2));

		}

		public async Task<TblLnsLensIndexRSphDto> GetById(int id)
        {
            var TblLnsLensIndexRSph =TblLnsLensIndexRSphRepository.GetByKey(id);
            return TblLnsLensIndexRSphConverter.ToDtoWithRelated(TblLnsLensIndexRSph,3);
        }

        public Task Update(TblLnsLensIndexRSphDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsLensIndexRSphDto");
			return TblLnsLensIndexRSphRepository.Update(item);
		}

        public Task Update(List<TblLnsLensIndexRSphDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
