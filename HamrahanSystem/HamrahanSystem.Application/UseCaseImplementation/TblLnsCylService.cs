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
    public class TblLnsCylService(ITblLnsCylRepository TblLnsCylRepository, ICacheService cacheService) :ITblLnsCylService
    {
        public Task Add(TblLnsCylDto dto)
        {
			var item = dto.ToEntity();
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");
            return TblLnsCylRepository.Add(item);
		}

        public Task Add(List<TblLnsCylDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblLnsCylDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsCylDto>> GetAll()
        {
            var TblLnsCyls=TblLnsCylRepository.GetAll();
            
            return TblLnsCylConverter.ToDtos(TblLnsCyls);

        }
		public Task<(List<TblLnsCylDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var TblLnsCyles = TblLnsCylRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsCylConverter.ToDtos(TblLnsCyles.Result.Item1), TblLnsCyles.Result.Item2));

		}

		public async Task<TblLnsCylDto> GetById(int id)
        {
            var TblLnsCyl =TblLnsCylRepository.GetByKey(id);
            return TblLnsCylConverter.ToDto(TblLnsCyl);
        }

        public Task Update(TblLnsCylDto dto)
        {
			var item = dto.ToEntity();
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");
            return TblLnsCylRepository.Update(item);
		}

        public Task Update(List<TblLnsCylDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
