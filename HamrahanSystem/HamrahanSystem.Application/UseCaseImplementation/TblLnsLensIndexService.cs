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
    public class TblLnsLensIndexService(ITblLnsLensIndexRepository tblLnsLensIndexRepository) :ITblLnsLensIndexService
    {
        public Task Add(TblLnsLensIndexDto dto)
        {
			var item = dto.ToEntity();
			return tblLnsLensIndexRepository.Add(item);
		}

        public Task Add(List<TblLnsLensIndexDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return tblLnsLensIndexRepository.Delete(id);
            
        }

        public Task Delete(List<TblLnsLensIndexDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsLensIndexDto>> GetAll()
        {
            var TblLnsLensIndexs= tblLnsLensIndexRepository.GetAll();
            
            return TblLnsLensIndexConverter.ToDtos(TblLnsLensIndexs);

        }
		public Task<(List<TblLnsLensIndexDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsBrands = tblLnsLensIndexRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((TblLnsLensIndexConverter.ToDtos(TblLnsBrands.Result.Item1), TblLnsBrands.Result.Item2));

		}

		public async Task<TblLnsLensIndexDto> GetById(int id)
        {
            var TblLnsLensIndex =tblLnsLensIndexRepository.GetByKey(id);
            return TblLnsLensIndexConverter.ToDto(TblLnsLensIndex);
        }

        public Task Update(TblLnsLensIndexDto dto)
        {
			var item = dto.ToEntity();
			return tblLnsLensIndexRepository.Update(item);
		}

        public Task Update(List<TblLnsLensIndexDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
