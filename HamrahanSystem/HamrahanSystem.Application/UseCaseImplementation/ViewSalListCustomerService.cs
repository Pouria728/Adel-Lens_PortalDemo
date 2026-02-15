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
    public class ViewSalListCustomerService(IViewSalListCustomerRepository ViewSalListCustomerRepository,ICacheService cacheService) :IViewSalListCustomerService
    {
        public Task Add(ViewSalListCustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<ViewSalListCustomerDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<ViewSalListCustomerDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ViewSalListCustomerDto>> GetAll()
        {
			var cache = cacheService.GetData<List<ViewSalListCustomerDto>>("ViewSalListCustomerDto");
            if (cache == null)
            {

				var ViewSalListCustomers = ViewSalListCustomerRepository.GetAll().ToList();
				var items = ViewSalListCustomerConverter.ToDtos(ViewSalListCustomers);
				cacheService.SetData<List<ViewSalListCustomerDto>>("ViewSalListCustomerDto", items);
				return items;

			}
            return cache;

        }
		public async Task<List<ViewSalListCustomerDto>> GetByName(string name)
		{
			var cache = cacheService.GetData<List<ViewSalListCustomerDto>>("ViewSalListCustomerDto");
			if (cache == null)
			{

				var ViewSalListCustomers = ViewSalListCustomerRepository.GetAll().ToList();
				var items = ViewSalListCustomerConverter.ToDtos(ViewSalListCustomers);
				cacheService.SetData<List<ViewSalListCustomerDto>>("ViewSalListCustomerDto", items);
				return items.Where(x => x.NameFormal.Contains(name)).ToList();

			}
			return cache.Where(x => x.NameFormal.Contains(name)).ToList();

		}

		public async Task<ViewSalListCustomerDto> GetById(int id)
        {
            var ViewSalListCustomer = ViewSalListCustomerRepository.GetByKey(id);
            return ViewSalListCustomerConverter.ToDto(ViewSalListCustomer);
        }

        public Task Update(ViewSalListCustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<ViewSalListCustomerDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
