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
    public class TblLnsLensIndexRService(ITblLnsLensIndexRRepository tblLnsLensIndexRRepository) :ITblLnsLensIndexRService
    {
        public Task Add(TblLnsLensIndexRDto dto)
        {
			var item = dto.ToEntity();
			return tblLnsLensIndexRRepository.Add(item);
		}

        public Task Add(List<TblLnsLensIndexRDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return tblLnsLensIndexRRepository.Delete(id);
        }

        public Task Delete(List<TblLnsLensIndexRDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsLensIndexRDto>> GetAll()
        {
            var TblLnsLensIndexRs=tblLnsLensIndexRRepository.GetAll();
            
            return TblLnsLensIndexRConverter.ToDtos(TblLnsLensIndexRs);

        }
		public Task<(List<TblLnsLensIndexRDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsBrands = tblLnsLensIndexRRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsLensIndexRConverter.ToDtos(TblLnsBrands.Result.Item1), TblLnsBrands.Result.Item2));

		}

		public async Task<TblLnsLensIndexRDto> GetById(int id)
        {
            var TblLnsLensIndexR =tblLnsLensIndexRRepository.GetByKey(id);
            return TblLnsLensIndexRConverter.ToDto(TblLnsLensIndexR);
        }

        public Task Update(TblLnsLensIndexRDto dto)
        {
			var item = dto.ToEntity();
			return tblLnsLensIndexRRepository.Update(item);
		}

        public Task Update(List<TblLnsLensIndexRDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
