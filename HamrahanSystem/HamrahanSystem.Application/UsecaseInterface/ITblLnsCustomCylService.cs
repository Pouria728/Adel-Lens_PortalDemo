using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomCylService
    {
        Task<IEnumerable<TblLnsCustomCylDto>> GetAll();
        Task<(List<TblLnsCustomCylDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomCylDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomCylDto> dto);
        Task Update(TblLnsCustomCylDto dto);
        Task Update(List<TblLnsCustomCylDto> dto);
        Task Add(TblLnsCustomCylDto dto);
        Task Add(List<TblLnsCustomCylDto> dto);
    }
}
