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
    public class TblLnsLensIndexMaterialTypeService(ITblLnsLensIndexMaterialTypeRepository TblLnsLensIndexMaterialTypeRepository) :ITblLnsLensIndexMaterialTypeService
    {
        public Task Add(TblLnsLensIndexMaterialTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsLensIndexMaterialTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsLensIndexMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsLensIndexMaterialTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsLensIndexMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsLensIndexMaterialTypeDto>> GetAll()
        {
            var TblLnsLensIndexMaterialTypes=TblLnsLensIndexMaterialTypeRepository.GetAll();
            
            return TblLnsLensIndexMaterialTypeConverter.ToDtos(TblLnsLensIndexMaterialTypes);

        }
		public Task<(List<TblLnsLensIndexMaterialTypeDto>, int)> GetAll(int designTypeLensIndexId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsBrands = TblLnsLensIndexMaterialTypeRepository.GetAll(designTypeLensIndexId, maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((TblLnsLensIndexMaterialTypeConverter.ToDtosWithRelated(TblLnsBrands.Result.Item1,4), TblLnsBrands.Result.Item2));

		}

		public async Task<TblLnsLensIndexMaterialTypeDto> GetById(int id)
        {
            var TblLnsLensIndexMaterialType =TblLnsLensIndexMaterialTypeRepository.GetByKey(id);
            return TblLnsLensIndexMaterialTypeConverter.ToDtoWithRelated(TblLnsLensIndexMaterialType,5);
        }

        public Task Update(TblLnsLensIndexMaterialTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsLensIndexMaterialTypeRepository.Update(item);
		}

        public Task Update(List<TblLnsLensIndexMaterialTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
