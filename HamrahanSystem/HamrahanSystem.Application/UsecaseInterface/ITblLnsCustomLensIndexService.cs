
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomLensIndexService
    {
        Task Add(TblLnsCustomLensIndexDto dto);
        Task Add(List<TblLnsCustomLensIndexDto> dto);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomLensIndexDto> dto);
        Task<IEnumerable<TblLnsCustomLensIndexDto>> GetAll();
        Task<(List<TblLnsCustomLensIndexDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomLensIndexDto> GetById(int id);
        Task Update(TblLnsCustomLensIndexDto dto);
        Task Update(List<TblLnsCustomLensIndexDto> dto);
    }
}
