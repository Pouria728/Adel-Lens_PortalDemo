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
    public class BaseRequestFieldService(IBaseRequestFieldRepository BaseRequestFieldRepository,ICacheService cacheService) :IBaseRequestFieldService
    {
        public Task Add(BaseRequestFieldDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("BaseRequestFieldDto");
			return BaseRequestFieldRepository.Add(item);
		}

        public Task Add(List<BaseRequestFieldDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<BaseRequestFieldDto> dto)
        {
            throw new NotImplementedException();
        }
		public async Task<List<BaseRequestFieldDto>> GetAllActive()
		{
            
			var cache = cacheService.GetData<List<BaseRequestFieldDto>>("BaseRequestFieldDto");
            if (cache == null)
            {
                var BaseRequestFields = BaseRequestFieldRepository.GetAll().ToList();
                var items = BaseRequestFieldConverter.ToDtosWithRelated(BaseRequestFields, 1);
				cacheService.SetData<List<BaseRequestFieldDto>>("BaseRequestFieldDto", items);
				return items;
            }
            else
				return cache;
		}

		public async Task<IEnumerable<BaseRequestFieldDto>> GetAll()
        {
            var BaseRequestFields=BaseRequestFieldRepository.GetAll();
            
            return BaseRequestFieldConverter.ToDtos(BaseRequestFields);

        }
		public Task<(List<BaseRequestFieldDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var BaseRequestFields = BaseRequestFieldRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((BaseRequestFieldConverter.ToDtos(BaseRequestFields.Result.Item1), BaseRequestFields.Result.Item2));

		}

		public async Task<BaseRequestFieldDto> GetById(int id)
        {
            var BaseRequestField =BaseRequestFieldRepository.GetByKey(id);
            return BaseRequestFieldConverter.ToDtoWithRelated(BaseRequestField,1);
        }

        public Task Update(BaseRequestFieldDto dto)
        {
            var item = dto.ToEntity();
            cacheService.RemoveData("BaseRequestFieldDto");
            return BaseRequestFieldRepository.Update(item);
            
        }

        public Task Update(List<BaseRequestFieldDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
