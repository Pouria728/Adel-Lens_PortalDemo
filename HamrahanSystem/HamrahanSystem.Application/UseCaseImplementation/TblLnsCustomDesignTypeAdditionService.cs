
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsCustomDesignTypeAdditionService(ITblLnsCustomDesignTypeAdditionRepository tblLnsCustomDesignTypeAdditionRepository) : ITblLnsCustomDesignTypeAdditionService
    {
        public Task Add(TblLnsCustomDesignTypeAdditionDto dto)
        {
            var item = TblLnsCustomDesignTypeAdditionConverter.ToEntity(dto);
            return tblLnsCustomDesignTypeAdditionRepository.Add(item);
        }

        public Task Add(List<TblLnsCustomDesignTypeAdditionDto> dto)
        {
            var item = TblLnsCustomDesignTypeAdditionConverter.ToEntities(dto);
            return tblLnsCustomDesignTypeAdditionRepository.Add(item);
        }

        public Task Delete(int id)
        {
            return tblLnsCustomDesignTypeAdditionRepository.Delete(id);
        }

        public Task Delete(List<TblLnsCustomDesignTypeAdditionDto> dto)
        {
            var item = TblLnsCustomDesignTypeAdditionConverter.ToEntities(dto);
            return tblLnsCustomDesignTypeAdditionRepository.Delete(item);
        }

        public async Task<IEnumerable<TblLnsCustomDesignTypeAdditionDto>> GetAll()
        {
            var items = tblLnsCustomDesignTypeAdditionRepository.GetAll();
            return TblLnsCustomDesignTypeAdditionConverter.ToDtos(items);
        }

        public Task<(List<TblLnsCustomDesignTypeAdditionDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
        {
            var result = tblLnsCustomDesignTypeAdditionRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
            return Task.FromResult((TblLnsCustomDesignTypeAdditionConverter.ToDtos(result.Result.Item1), result.Result.Item2));
        }

        public async Task<TblLnsCustomDesignTypeAdditionDto> GetById(int id)
        {
            var item = tblLnsCustomDesignTypeAdditionRepository.GetByKey(id);
            return TblLnsCustomDesignTypeAdditionConverter.ToDto(item);
        }

        public Task Update(TblLnsCustomDesignTypeAdditionDto dto)
        {
            var item = TblLnsCustomDesignTypeAdditionConverter.ToEntity(dto);
            return tblLnsCustomDesignTypeAdditionRepository.Update(item);
        }

        public Task Update(List<TblLnsCustomDesignTypeAdditionDto> dto)
        {
            var item = TblLnsCustomDesignTypeAdditionConverter.ToEntities(dto);
            return tblLnsCustomDesignTypeAdditionRepository.Update(item);
        }
    }
}
