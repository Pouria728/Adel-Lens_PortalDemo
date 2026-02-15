using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
	public class TblLnsBrandLensTypeService(ITblLnsBrandLensTypeRepository TblLnsBrandLensTypeRepository,ICacheService cacheService) : ITblLnsBrandLensTypeService
	{
		public Task Add(TblLnsBrandLensTypeDto dto)
		{
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsBrandLensTypeDto");
			return TblLnsBrandLensTypeRepository.Add(item);
		}

		public Task Add(List<TblLnsBrandLensTypeDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
            return TblLnsBrandLensTypeRepository.Delete(id);
        }

		public Task Delete(List<TblLnsBrandLensTypeDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<List<TblLnsBrandLensTypeDto>> GetAll()
		{
			var cache = cacheService.GetData<List<TblLnsBrandLensTypeDto>>("TblLnsBrandLensTypeDto");
			if (cache == null)
			{
				var TblLnsBrandLensTypes = TblLnsBrandLensTypeRepository.GetAll();
				var items = TblLnsBrandLensTypeConverter.ToDtosWithRelated(TblLnsBrandLensTypes,1);
				cacheService.SetData<List<TblLnsBrandLensTypeDto>>("TblLnsBrandLensTypeDto", items);
				return items;
			}
			else
				return cache;
		}
		public Task<(List<TblLnsBrandLensTypeDto>, int)> GetAll(int BrandId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsBrands = TblLnsBrandLensTypeRepository.GetAll(BrandId, maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsBrandLensTypeConverter.ToDtosWithRelated(TblLnsBrands.Result.Item1, 1), TblLnsBrands.Result.Item2));

		}

		public async Task<TblLnsBrandLensTypeDto> GetById(int id)
		{
			var TblLnsBrandLensType = TblLnsBrandLensTypeRepository.GetByKey(id);
			return TblLnsBrandLensTypeConverter.ToDtoWithRelated(TblLnsBrandLensType, 1);
		}

		public Task Update(TblLnsBrandLensTypeDto dto)
		{
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsBrandLensTypeDto");
			return TblLnsBrandLensTypeRepository.Update(item);
		}

		public Task Update(List<TblLnsBrandLensTypeDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
