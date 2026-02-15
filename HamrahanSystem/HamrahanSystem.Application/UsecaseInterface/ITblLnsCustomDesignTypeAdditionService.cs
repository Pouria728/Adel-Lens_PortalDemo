
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomDesignTypeAdditionService
    {
        Task<IEnumerable<TblLnsCustomDesignTypeAdditionDto>> GetAll();
        Task<(List<TblLnsCustomDesignTypeAdditionDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomDesignTypeAdditionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomDesignTypeAdditionDto> dto);

        Task Update(TblLnsCustomDesignTypeAdditionDto dto);
        Task Update(List<TblLnsCustomDesignTypeAdditionDto> dto);
        Task Add(TblLnsCustomDesignTypeAdditionDto dto);
        Task Add(List<TblLnsCustomDesignTypeAdditionDto> dto);
    }
}
