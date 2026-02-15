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
    public class TblLnsSphCylService(ITblLnsSphCylRepository TblLnsSphCylRepository,ICacheService cacheService) :ITblLnsSphCylService
    {
        public Task Add(TblLnsSphCylDto dto)
        {
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");
            var item = dto.ToEntity();
			return TblLnsSphCylRepository.Add(item);
		}

        public Task Add(List<TblLnsSphCylDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsSphCylRepository.Delete(id);
        }

        public Task Delete(List<TblLnsSphCylDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblLnsSphCylDto>> GetAll()
        {
			var cache = cacheService.GetData<List<TblLnsSphCylDto>>("TblLnsSphCylDto");
			if (cache == null)
			{
				var TblLnsSphCyls=TblLnsSphCylRepository.GetAll().ToList();
				var items = TblLnsSphCylConverter.ToDtosWithRelated(TblLnsSphCyls, 1);
				cacheService.SetData<List<TblLnsSphCylDto>>("TblLnsSphCylDto", items);
				return items;
			}
			else
				return cache;

        }
		public Task<(List<TblLnsSphCylDto>, int)> GetAll(int lensIndexRSphId, int? maxResult, int? Page, int? rowInPage,string sort, string sidx)
		{
			var TblLnsSphCyls = TblLnsSphCylRepository.GetAll(lensIndexRSphId, maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsSphCylConverter.ToDtosWithRelated(TblLnsSphCyls.Result.Item1,4), TblLnsSphCyls.Result.Item2));

		}

		public async Task<TblLnsSphCylDto> GetById(int id)
        {
            var TblLnsSphCyl =TblLnsSphCylRepository.GetByKey(id);
            return TblLnsSphCylConverter.ToDtoWithRelated(TblLnsSphCyl,5);
        }

        public Task Update(TblLnsSphCylDto dto)
        {
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");
            var item = dto.ToEntity();
			return TblLnsSphCylRepository.Update(item);
		}

        public Task Update(List<TblLnsSphCylDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
