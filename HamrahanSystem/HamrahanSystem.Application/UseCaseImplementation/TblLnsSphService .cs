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
    public class TblLnsSphService(ITblLnsSphRepository TblLnsSphRepository, ICacheService cacheService) : ITblLnsSphService
    {
        public Task Add(TblLnsSphDto dto)
        {
            var item = dto.ToEntity();
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");

            return TblLnsSphRepository.Add(item);
        }

        public Task Add(List<TblLnsSphDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsSphRepository.Delete(id);
            
        }

        public Task Delete(List<TblLnsSphDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsSphDto>> GetAll()
        {
            var TblLnsSphs = TblLnsSphRepository.GetAll();

            return TblLnsSphConverter.ToDtos(TblLnsSphs);

        }
        public Task<(List<TblLnsSphDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
        {
            var TblLnsSphes = TblLnsSphRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

            return Task.FromResult((TblLnsSphConverter.ToDtos(TblLnsSphes.Result.Item1), TblLnsSphes.Result.Item2));

        }

        public async Task<TblLnsSphDto> GetById(int id)
        {
            var TblLnsSph = TblLnsSphRepository.GetByKey(id);
            return TblLnsSphConverter.ToDto(TblLnsSph);
        }

        public Task Update(TblLnsSphDto dto)
        {
            cacheService.RemoveData("TblLnsSphCylDto");
            cacheService.RemoveData("TblLnsLensIndexRSphDto");
            var item = dto.ToEntity();
            return TblLnsSphRepository.Update(item);
        }

        public Task Update(List<TblLnsSphDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
