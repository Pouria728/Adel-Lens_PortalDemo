using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomSphService(ITblLnsCustomSphRepository tblLnsCustomSphRepository) : ITblLnsCustomSphService
    {
        public Task Add(TblLnsCustomSphDto dto)
        {
            var item = TblLnsCustomSphConverter.ToEntity(dto);
            return tblLnsCustomSphRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomSphDto> dto)
        {
            var item = TblLnsCustomSphConverter.ToEntities(dto);
            return tblLnsCustomSphRepository.Add(item);
        }

        public Task Delete(int id)
        {
            return tblLnsCustomSphRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomSphDto> dto)
        {
            var item = TblLnsCustomSphConverter.ToEntities(dto);
            return tblLnsCustomSphRepository.Delete(item);
        }

        public async Task<IEnumerable<TblLnsCustomSphDto>> GetAll()
        {
            var items = tblLnsCustomSphRepository.GetAll();
            return TblLnsCustomSphConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomSphDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx)
        {
            var result = tblLnsCustomSphRepository.GetAll(maxResult, page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomSphConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomSphDto> GetById(int id)
        {
            var item = tblLnsCustomSphRepository.GetByKey(id);
            return TblLnsCustomSphConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomSphDto dto)
        {
            var item = TblLnsCustomSphConverter.ToEntity(dto);
            return tblLnsCustomSphRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomSphDto> dto)
        {
            var item = TblLnsCustomSphConverter.ToEntities(dto);
            return tblLnsCustomSphRepository.Update(item);
        }
    }
}
