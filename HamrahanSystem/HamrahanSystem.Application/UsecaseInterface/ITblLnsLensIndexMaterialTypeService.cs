using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensIndexMaterialTypeService
    {
        Task<IEnumerable<TblLnsLensIndexMaterialTypeDto>> GetAll();
		Task<(List<TblLnsLensIndexMaterialTypeDto>, int)> GetAll(int designTypeLensIndexId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensIndexMaterialTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensIndexMaterialTypeDto> dto);

        Task Update (TblLnsLensIndexMaterialTypeDto dto);
        Task Update(List<TblLnsLensIndexMaterialTypeDto> dto);
        Task Add(TblLnsLensIndexMaterialTypeDto dto);
        Task Add(List<TblLnsLensIndexMaterialTypeDto> dto);

    }
}
