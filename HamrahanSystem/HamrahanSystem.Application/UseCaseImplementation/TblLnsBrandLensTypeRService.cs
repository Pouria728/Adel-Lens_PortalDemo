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
    public class TblLnsBrandLensTypeRService(ITblLnsBrandLensTypeRRepository TblLnsBrandLensTypeRRepository,ICacheService cacheService) :ITblLnsBrandLensTypeRService
    {
        public Task Add(TblLnsBrandLensTypeRDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsBrandLensTypeRDto");
			return TblLnsBrandLensTypeRRepository.Add(item);
		}

        public Task Add(List<TblLnsBrandLensTypeRDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            var TblLnsBrandLensTypeR = TblLnsBrandLensTypeRRepository.Delete(id);
            return TblLnsBrandLensTypeR;
        }

        public Task Delete(List<TblLnsBrandLensTypeRDto> dto)
        {
            
            throw new NotImplementedException();
        }

        public async Task<List<TblLnsBrandLensTypeRDto>> GetAll()
        {
			var cache = cacheService.GetData<List<TblLnsBrandLensTypeRDto>>("TblLnsBrandLensTypeRDto");
            if (cache == null)
            {
                var TblLnsBrandLensTypeRs = TblLnsBrandLensTypeRRepository.GetAll().ToList();
                var items = TblLnsBrandLensTypeRConverter.ToDtosWithRelated(TblLnsBrandLensTypeRs,1);
                cacheService.SetData<List<TblLnsBrandLensTypeRDto>>("TblLnsBrandLensTypeRDto", items);
                return items;
            }
            else
                return cache;

        }
		public Task<(List<TblLnsBrandLensTypeRDto>, int)> GetAll(int BrandId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsBrandLensTypeR = TblLnsBrandLensTypeRRepository.GetAll(BrandId,maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsBrandLensTypeRConverter.ToDtosWithRelated(TblLnsBrandLensTypeR.Result.Item1,1), TblLnsBrandLensTypeR.Result.Item2));

		}

		public async Task<TblLnsBrandLensTypeRDto> GetById(int id)
        {
            var TblLnsBrandLensTypeR =TblLnsBrandLensTypeRRepository.GetByKey(id);
            return TblLnsBrandLensTypeRConverter.ToDtoWithRelated(TblLnsBrandLensTypeR,1);
        }

        public Task Update(TblLnsBrandLensTypeRDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsBrandLensTypeRDto");
			return TblLnsBrandLensTypeRRepository.Update(item);
		}

        public Task Update(List<TblLnsBrandLensTypeRDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
