using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsBrandService(ITblLnsBrandRepository TblLnsBrandRepository,ICacheService cacheService) :ITblLnsBrandService
    {
        public Task Add(TblLnsBrandDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("TblLnsBrandDto");
			return TblLnsBrandRepository.Add(item);
		}

        public Task Add(List<TblLnsBrandDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsBrandRepository.Delete(id);
            
        }

        public Task Delete(List<TblLnsBrandDto> dto)
        {
            throw new NotImplementedException();
        }
		public async Task<List<TblLnsBrandDto>> GetAllActive()
		{
            
			var cache = cacheService.GetData<List<TblLnsBrandDto>>("TblLnsBrandDto");
            if (cache == null)
            {
                var TblLnsBrands = TblLnsBrandRepository.GetAllActive().ToList();
                var items = TblLnsBrandConverter.ToDtosWithRelated(TblLnsBrands, 1);
				cacheService.SetData<List<TblLnsBrandDto>>("TblLnsBrandDto", items);
				return items;
            }
            else
				return cache;
		}

		public async Task<IEnumerable<TblLnsBrandDto>> GetAll()
        {
            var TblLnsBrands=TblLnsBrandRepository.GetAll();
            
            return TblLnsBrandConverter.ToDtos(TblLnsBrands);

        }
		public Task<(List<TblLnsBrandDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage,string sort ,string sidx)
		{
			var TblLnsBrands = TblLnsBrandRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsBrandConverter.ToDtos(TblLnsBrands.Result.Item1), TblLnsBrands.Result.Item2));

		}

		public async Task<TblLnsBrandDto> GetById(int id)
        {
            var TblLnsBrand =TblLnsBrandRepository.GetByKey(id);
            return TblLnsBrandConverter.ToDto(TblLnsBrand);
        }

        public Task Update(TblLnsBrandDto dto)
        {
            var item = dto.ToEntity();
            cacheService.RemoveData("TblLnsBrandDto");
            return TblLnsBrandRepository.Update(item);
            
        }

        public Task Update(List<TblLnsBrandDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
