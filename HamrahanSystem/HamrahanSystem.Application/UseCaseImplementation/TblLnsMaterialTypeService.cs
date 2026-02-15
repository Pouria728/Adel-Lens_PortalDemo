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
    public class TblLnsMaterialTypeService(ITblLnsMaterialTypeRepository TblLnsMaterialTypeRepository) :ITblLnsMaterialTypeService
    {
        public Task Add(TblLnsMaterialTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsMaterialTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsMaterialTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsMaterialTypeDto>> GetAll()
        {
            var TblLnsMaterialTypes=TblLnsMaterialTypeRepository.GetAll();
            
            return TblLnsMaterialTypeConverter.ToDtos(TblLnsMaterialTypes);

        }

        public async Task<TblLnsMaterialTypeDto> GetById(int id)
        {
            var TblLnsMaterialType =TblLnsMaterialTypeRepository.GetByKey(id);
            return TblLnsMaterialTypeConverter.ToDto(TblLnsMaterialType);
        }
		public Task<(List<TblLnsMaterialTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var tblLnsMaterialTypes = TblLnsMaterialTypeRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((TblLnsMaterialTypeConverter.ToDtos(tblLnsMaterialTypes.Result.Item1), tblLnsMaterialTypes.Result.Item2));

		}

		public Task Update(TblLnsMaterialTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsMaterialTypeRepository.Update(item);
		}

        public Task Update(List<TblLnsMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
