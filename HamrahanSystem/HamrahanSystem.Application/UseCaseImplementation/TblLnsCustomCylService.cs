using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomCylService(ITblLnsCustomCylRepository tblLnsCustomCylRepository) : ITblLnsCustomCylService
    {
        public Task Add(TblLnsCustomCylDto dto)
        {
            var item = TblLnsCustomCylConverter.ToEntity(dto);
            return tblLnsCustomCylRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomCylDto> dto)
        {
            var item = TblLnsCustomCylConverter.ToEntities(dto);
            return tblLnsCustomCylRepository.Add(item);
        }

        public Task Delete(int id)
        {
            return tblLnsCustomCylRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomCylDto> dto)
        {
            var item = TblLnsCustomCylConverter.ToEntities(dto);
            return tblLnsCustomCylRepository.Delete(item);
        }

        public async Task<IEnumerable<TblLnsCustomCylDto>> GetAll()
        {
            var items = tblLnsCustomCylRepository.GetAll();
            return TblLnsCustomCylConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomCylDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var result = tblLnsCustomCylRepository.GetAll(maxResult, page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomCylConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomCylDto> GetById(int id)
        {
            var item = tblLnsCustomCylRepository.GetByKey(id);
            return TblLnsCustomCylConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomCylDto dto)
        {
            var item = TblLnsCustomCylConverter.ToEntity(dto);
            return tblLnsCustomCylRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomCylDto> dto)
        {
            var item = TblLnsCustomCylConverter.ToEntities(dto);
            return tblLnsCustomCylRepository.Update(item);
        }
    }
}
