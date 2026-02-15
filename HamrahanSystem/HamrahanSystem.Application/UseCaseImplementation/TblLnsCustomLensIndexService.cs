
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomLensIndexService(ITblLnsCustomLensIndexRepository tblLnsCustomLensIndexRepository) : ITblLnsCustomLensIndexService
    {
        public Task Add(TblLnsCustomLensIndexDto dto)
        {
            var item = TblLnsCustomLensIndexConverter.ToEntity(dto);
            return tblLnsCustomLensIndexRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomLensIndexDto> dto)
        {
            var item = TblLnsCustomLensIndexConverter.ToEntities(dto);
            return tblLnsCustomLensIndexRepository.Add(item);
        }

        public Task Delete(int id)
        {
            return tblLnsCustomLensIndexRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomLensIndexDto> dto)
        {
            var item = TblLnsCustomLensIndexConverter.ToEntities(dto);
            return tblLnsCustomLensIndexRepository.Delete(item);
        }

        public async Task<IEnumerable<TblLnsCustomLensIndexDto>> GetAll()
        {
            var items = tblLnsCustomLensIndexRepository.GetAll();
            return TblLnsCustomLensIndexConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomLensIndexDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
        {
            var result = tblLnsCustomLensIndexRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomLensIndexConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomLensIndexDto> GetById(int id)
        {
            var item = tblLnsCustomLensIndexRepository.GetByKey(id);
            return TblLnsCustomLensIndexConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomLensIndexDto dto)
        {
            var item = TblLnsCustomLensIndexConverter.ToEntity(dto);
            return tblLnsCustomLensIndexRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomLensIndexDto> dto)
        {
            var item = TblLnsCustomLensIndexConverter.ToEntities(dto);
            return tblLnsCustomLensIndexRepository.Update(item);
        }
    }
}
