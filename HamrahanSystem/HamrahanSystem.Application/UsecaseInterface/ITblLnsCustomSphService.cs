using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomSphService
    {
        Task<IEnumerable<TblLnsCustomSphDto>> GetAll();
        Task<(List<TblLnsCustomSphDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomSphDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomSphDto> dto);
        Task Update(TblLnsCustomSphDto dto);
        Task Update(List<TblLnsCustomSphDto> dto);
        Task Add(TblLnsCustomSphDto dto);
        Task Add(List<TblLnsCustomSphDto> dto);
    }
}
