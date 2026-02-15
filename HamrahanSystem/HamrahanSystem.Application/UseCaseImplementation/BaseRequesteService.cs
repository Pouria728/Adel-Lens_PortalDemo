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
    public class BaseRequesteService(IBaseRequesteRepository BaseRequesteRepository,ICacheService cacheService) :IBaseRequesteService
    {
        public Task Add(BaseRequesteDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("BaseRequesteDto");
			return BaseRequesteRepository.Add(item);
		}

        public Task Add(List<BaseRequesteDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<BaseRequesteDto> dto)
        {
            throw new NotImplementedException();
        }
		public async Task<List<BaseRequesteDto>> GetAllActive()
		{
            
			var cache = cacheService.GetData<List<BaseRequesteDto>>("BaseRequesteDto");
            if (cache == null)
            {
                var BaseRequestes = BaseRequesteRepository.GetAll().ToList();
                var items = BaseRequesteConverter.ToDtosWithRelated(BaseRequestes, 1);
				cacheService.SetData<List<BaseRequesteDto>>("BaseRequesteDto", items);
				return items;
            }
            else
				return cache;
		}

		public async Task<List<BaseRequesteDto>> GetAll()
        {
            var BaseRequestes=BaseRequesteRepository.GetAll();
            
            return BaseRequesteConverter.ToDtos(BaseRequestes);

        }
		public Task<(List<BaseRequesteDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var BaseRequestes = BaseRequesteRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((BaseRequesteConverter.ToDtos(BaseRequestes.Result.Item1), BaseRequestes.Result.Item2));

		}

		public async Task<BaseRequesteDto> GetById(int id)
        {
            var BaseRequeste =BaseRequesteRepository.GetByKey(id);
            return BaseRequesteConverter.ToDto(BaseRequeste);
        }

        public Task Update(BaseRequesteDto dto)
        {
            var item = dto.ToEntity();
            cacheService.RemoveData("BaseRequesteDto");
            return BaseRequesteRepository.Update(item);
            
        }

        public Task Update(List<BaseRequesteDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
