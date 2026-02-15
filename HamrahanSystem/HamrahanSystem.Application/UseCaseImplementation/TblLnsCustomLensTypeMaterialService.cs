
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomLensTypeMaterialService(ITblLnsCustomLensTypeMaterialRepository tblLnsCustomLensTypeMaterialRepository) : ITblLnsCustomLensTypeMaterialService
    {
        public Task Add(TblLnsCustomLensTypeMaterialDto dto)
        {
            var item = TblLnsCustomLensTypeMaterialConverter.ToEntity(dto);
            return tblLnsCustomLensTypeMaterialRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomLensTypeMaterialDto> dto)
        {
            var item = TblLnsCustomLensTypeMaterialConverter.ToEntities(dto);
            return tblLnsCustomLensTypeMaterialRepository.Add(item);
        }

        public Task Delete(int id)
        {
            return tblLnsCustomLensTypeMaterialRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomLensTypeMaterialDto> dto)
        {
            var item = TblLnsCustomLensTypeMaterialConverter.ToEntities(dto);
            return tblLnsCustomLensTypeMaterialRepository.Delete(item);
        }

        public async Task<IEnumerable<TblLnsCustomLensTypeMaterialDto>> GetAll()
        {
            var items = tblLnsCustomLensTypeMaterialRepository.GetAll();
            return TblLnsCustomLensTypeMaterialConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomLensTypeMaterialDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
        {
            var result = tblLnsCustomLensTypeMaterialRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomLensTypeMaterialConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomLensTypeMaterialDto> GetById(int id)
        {
            var item = tblLnsCustomLensTypeMaterialRepository.GetByKey(id);
            return TblLnsCustomLensTypeMaterialConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomLensTypeMaterialDto dto)
        {
            var item = TblLnsCustomLensTypeMaterialConverter.ToEntity(dto);
            return tblLnsCustomLensTypeMaterialRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomLensTypeMaterialDto> dto)
        {
            var item = TblLnsCustomLensTypeMaterialConverter.ToEntities(dto);
            return tblLnsCustomLensTypeMaterialRepository.Update(item);
        }
    }
}
