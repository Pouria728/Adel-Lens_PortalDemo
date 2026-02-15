
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomLensTypeMaterialService
    {
        Task<IEnumerable<TblLnsCustomLensTypeMaterialDto>> GetAll();
        Task<(List<TblLnsCustomLensTypeMaterialDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomLensTypeMaterialDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomLensTypeMaterialDto> dto);

        Task Update(TblLnsCustomLensTypeMaterialDto dto);
        Task Update(List<TblLnsCustomLensTypeMaterialDto> dto);
        Task Add(TblLnsCustomLensTypeMaterialDto dto);
        Task Add(List<TblLnsCustomLensTypeMaterialDto> dto);
    }
}
