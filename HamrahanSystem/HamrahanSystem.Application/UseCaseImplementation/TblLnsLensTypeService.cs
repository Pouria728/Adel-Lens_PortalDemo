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
    public class TblLnsLensTypeService(ITblLnsLensTypeRepository TblLnsLensTypeRepository) :ITblLnsLensTypeService
    {
        public Task Add(TblLnsLensTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsLensTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsLensTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsLensTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsLensTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsLensTypeDto>> GetAll()
        {
            var TblLnsLensTypes=TblLnsLensTypeRepository.GetAll();
            
            return TblLnsLensTypeConverter.ToDtos(TblLnsLensTypes);

        }
		public Task<(List<TblLnsLensTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{

			var tblLnsLensTypes = TblLnsLensTypeRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
			return Task.FromResult((TblLnsLensTypeConverter.ToDtos(tblLnsLensTypes.Result.Item1), tblLnsLensTypes.Result.Item2));

		}

		public async Task<TblLnsLensTypeDto> GetById(int id)
        {
            var TblLnsLensType =TblLnsLensTypeRepository.GetByKey(id);
            return TblLnsLensTypeConverter.ToDto(TblLnsLensType);
        }

        public Task Update(TblLnsLensTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsLensTypeRepository.Update(item);
        }

        public Task Update(List<TblLnsLensTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
