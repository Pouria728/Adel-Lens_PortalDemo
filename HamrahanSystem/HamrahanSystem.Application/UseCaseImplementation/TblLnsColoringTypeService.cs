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
	public class TblLnsColoringTypeService(ITblLnsColoringTypeRepository TblLnsColoringTypeRepository) : ITblLnsColoringTypeService
	{
		public Task Add(TblLnsColoringTypeDto dto)
		{
			var item = dto.ToEntity();
			return TblLnsColoringTypeRepository.Add(item);
		}

		public Task Add(List<TblLnsColoringTypeDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task Delete(List<TblLnsColoringTypeDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TblLnsColoringTypeDto>> GetAll()
		{
			var TblLnsColoringTypes = TblLnsColoringTypeRepository.GetAll();

			return TblLnsColoringTypeConverter.ToDtos(TblLnsColoringTypes);

		}
		public Task<(List<TblLnsColoringTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{

			var tblLnsColoringTypes = TblLnsColoringTypeRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);
			return Task.FromResult((TblLnsColoringTypeConverter.ToDtos(tblLnsColoringTypes.Result.Item1), tblLnsColoringTypes.Result.Item2));

		}



		public async Task<TblLnsColoringTypeDto> GetById(int id)
		{
			var TblLnsColoringType = TblLnsColoringTypeRepository.GetByKey(id);
			return TblLnsColoringTypeConverter.ToDto(TblLnsColoringType);
		}

		public Task Update(TblLnsColoringTypeDto dto)
		{
			throw new NotImplementedException();
		}

		public Task Update(List<TblLnsColoringTypeDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
