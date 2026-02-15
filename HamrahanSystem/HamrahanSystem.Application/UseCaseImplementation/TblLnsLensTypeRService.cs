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
    public class TblLnsLensTypeRService(ITblLnsLensTypeRRepository TblLnsLensTypeRRepository) :ITblLnsLensTypeRService
    {
        public Task Add(TblLnsLensTypeRDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsLensTypeRRepository.Add(item);
		}

        public Task Add(List<TblLnsLensTypeRDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsLensTypeRRepository.Delete(id);
        }

        public Task Delete(List<TblLnsLensTypeRDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsLensTypeRDto>> GetAll()
        {
            var TblLnsLensTypeRs=TblLnsLensTypeRRepository.GetAll();
            
            return TblLnsLensTypeRConverter.ToDtos(TblLnsLensTypeRs);

        }
		public Task<(List<TblLnsLensTypeRDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{

			var tblLnsLensTypeRs = TblLnsLensTypeRRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
			return Task.FromResult((TblLnsLensTypeRConverter.ToDtos(tblLnsLensTypeRs.Result.Item1), tblLnsLensTypeRs.Result.Item2));

		}

		public async Task<TblLnsLensTypeRDto> GetById(int id)
        {
            var TblLnsLensTypeR =TblLnsLensTypeRRepository.GetByKey(id);
            return TblLnsLensTypeRConverter.ToDto(TblLnsLensTypeR);
        }

        public Task Update(TblLnsLensTypeRDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsLensTypeRDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
