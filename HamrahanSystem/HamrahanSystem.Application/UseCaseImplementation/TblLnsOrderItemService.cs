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
	public class TblLnsOrderItemService(ITblLnsOrderItemRepository TblLnsOrderItemRepository) : ITblLnsOrderItemService
	{
		public Task Add(TblLnsOrderItemDto dto)
		{
			throw new NotImplementedException();
		}

		public Task Add(List<TblLnsOrderItemDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task Delete(List<TblLnsOrderItemDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TblLnsOrderItemDto>> GetAll()
		{
			var TblLnsOrderItems = TblLnsOrderItemRepository.GetAll();

			return TblLnsOrderItemConverter.ToDtos(TblLnsOrderItems);

		}

		public async Task<TblLnsOrderItemDto> GetById(int id)
		{
			var TblLnsOrderItem = TblLnsOrderItemRepository.GetByKey(id);
			return TblLnsOrderItemConverter.ToDto(TblLnsOrderItem);
		}

		public Task Update(TblLnsOrderItemDto dto)
		{
			throw new NotImplementedException();
		}
		public Task UpdateProvidedQuantity(List<TblLnsOrderItemDto> dto)
		{
			TblLnsOrderItemRepository.UpdateProvidedQuantity(dto.ToEntities());
			return Task.CompletedTask;
		}
		public Task Update(List<TblLnsOrderItemDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
