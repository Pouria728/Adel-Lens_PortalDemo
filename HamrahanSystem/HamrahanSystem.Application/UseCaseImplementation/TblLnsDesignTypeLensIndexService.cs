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
	public class TblLnsDesignTypeLensIndexService(ITblLnsDesignTypeLensIndexRepository TblLnsDesignTypeLensIndexRepository) : ITblLnsDesignTypeLensIndexService
	{
		public Task Add(TblLnsDesignTypeLensIndexDto dto)
		{
			var item = dto.ToEntity();
			return TblLnsDesignTypeLensIndexRepository.Add(item);
		}

		public Task Add(List<TblLnsDesignTypeLensIndexDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			return TblLnsDesignTypeLensIndexRepository.Delete(id);
            
		}

		public Task Delete(List<TblLnsDesignTypeLensIndexDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TblLnsDesignTypeLensIndexDto>> GetAll()
		{
			var TblLnsDesignTypeLensIndexs = TblLnsDesignTypeLensIndexRepository.GetAll();

			return TblLnsDesignTypeLensIndexConverter.ToDtos(TblLnsDesignTypeLensIndexs);

		}
		public Task<(List<TblLnsDesignTypeLensIndexDto>, int)> GetAll(int designTypeId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsDesignTypeLensIndexs = TblLnsDesignTypeLensIndexRepository.GetAll(designTypeId, maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsDesignTypeLensIndexConverter.ToDtosWithRelated(TblLnsDesignTypeLensIndexs.Result.Item1, 3), TblLnsDesignTypeLensIndexs.Result.Item2));

		}


		public async Task<TblLnsDesignTypeLensIndexDto> GetById(int id)
		{
			var TblLnsDesignTypeLensIndex = TblLnsDesignTypeLensIndexRepository.GetByKey(id);
			return TblLnsDesignTypeLensIndexConverter.ToDtoWithRelated(TblLnsDesignTypeLensIndex, 3);
		}

		public Task Update(TblLnsDesignTypeLensIndexDto dto)
		{
			var item = dto.ToEntity();
			return TblLnsDesignTypeLensIndexRepository.Update(item);
		}

		public Task Update(List<TblLnsDesignTypeLensIndexDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
