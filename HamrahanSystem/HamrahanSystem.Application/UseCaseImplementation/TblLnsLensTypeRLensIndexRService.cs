using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
	public class TblLnsLensTypeRLensIndexRService(ITblLnsLensTypeRLensIndexRRepository TblLnsLensTypeRLensIndexRRepository,ICacheService cacheService) : ITblLnsLensTypeRLensIndexRService
	{
		public Task Add(TblLnsLensTypeRLensIndexRDto dto)
		{
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsLensTypeRLensIndexRDto");
			return TblLnsLensTypeRLensIndexRRepository.Add(item);
		}

		public Task Add(List<TblLnsLensTypeRLensIndexRDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
            return TblLnsLensTypeRLensIndexRRepository.Delete(id);
		}

		public Task Delete(List<TblLnsLensTypeRLensIndexRDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<List<TblLnsLensTypeRLensIndexRDto>> GetAll()
		{
			var cache = cacheService.GetData<List<TblLnsLensTypeRLensIndexRDto>>("TblLnsLensTypeRLensIndexRDto");
			if (cache == null)
			{
				var TblLnsLensTypeRLensIndexRs = TblLnsLensTypeRLensIndexRRepository.GetAll().ToList();
				var items = TblLnsLensTypeRLensIndexRConverter.ToDtosWithRelated(TblLnsLensTypeRLensIndexRs, 1);
				cacheService.SetData<List<TblLnsLensTypeRLensIndexRDto>>("TblLnsLensTypeRLensIndexRDto", items);
				return items;
			}
			else
				return cache;

		}
		public Task<(List<TblLnsLensTypeRLensIndexRDto>, int)> GetAll(int brandLensTypeRId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsLensTypeRLensIndexRs = TblLnsLensTypeRLensIndexRRepository.GetAll(brandLensTypeRId, maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((TblLnsLensTypeRLensIndexRConverter.ToDtosWithRelated(TblLnsLensTypeRLensIndexRs.Result.Item1, 2), TblLnsLensTypeRLensIndexRs.Result.Item2));

		}

		public async Task<TblLnsLensTypeRLensIndexRDto> GetById(int id)
		{
			var TblLnsLensTypeRLensIndexR = TblLnsLensTypeRLensIndexRRepository.GetByKey(id);
			return TblLnsLensTypeRLensIndexRConverter.ToDtoWithRelated(TblLnsLensTypeRLensIndexR, 2);
		}

		public Task Update(TblLnsLensTypeRLensIndexRDto dto)
		{
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsLensTypeRLensIndexRDto");
			return TblLnsLensTypeRLensIndexRRepository.Update(item);
		}

		public Task Update(List<TblLnsLensTypeRLensIndexRDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
