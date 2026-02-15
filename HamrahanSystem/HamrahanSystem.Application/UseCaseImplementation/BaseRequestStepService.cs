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
    public class BaseRequestStepService(IBaseRequestStepRepository BaseRequestStepRepository,ICacheService cacheService) :IBaseRequestStepService
    {
        public Task Add(BaseRequestStepDto dto)
        {
			var item = dto.ToEntity();
			cacheService.RemoveData("BaseRequestStepDto");
			return BaseRequestStepRepository.Add(item);
		}

        public Task Add(List<BaseRequestStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<BaseRequestStepDto> dto)
        {
            throw new NotImplementedException();
        }
		public async Task<List<BaseRequestStepDto>> GetAllActive()
		{
            
			var cache = cacheService.GetData<List<BaseRequestStepDto>>("BaseRequestStepDto");
            if (cache == null)
            {
                var BaseRequestSteps = BaseRequestStepRepository.GetAll().ToList();
                var items = BaseRequestStepConverter.ToDtosWithRelated(BaseRequestSteps, 1);
				cacheService.SetData<List<BaseRequestStepDto>>("BaseRequestStepDto", items);
				return items;
            }
            else
				return cache;
		}

		public async Task<IEnumerable<BaseRequestStepDto>> GetAll()
        {
            var BaseRequestSteps=BaseRequestStepRepository.GetAll();
            
            return BaseRequestStepConverter.ToDtos(BaseRequestSteps);

        }
		public Task<(List<BaseRequestStepDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var BaseRequestSteps = BaseRequestStepRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((BaseRequestStepConverter.ToDtos(BaseRequestSteps.Result.Item1), BaseRequestSteps.Result.Item2));

		}

		public async Task<BaseRequestStepDto> GetById(int id)
        {
            var BaseRequestStep =BaseRequestStepRepository.GetByKey(id);
            return BaseRequestStepConverter.ToDtoWithRelated(BaseRequestStep,1);
        }

        public Task Update(BaseRequestStepDto dto)
        {
            var item = dto.ToEntity();
            cacheService.RemoveData("BaseRequestStepDto");
            return BaseRequestStepRepository.Update(item);
            
        }

        public Task Update(List<BaseRequestStepDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
