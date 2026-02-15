using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsColoringTypeService
    {
        Task<IEnumerable<TblLnsColoringTypeDto>> GetAll();
        Task<(List<TblLnsColoringTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);

		Task<TblLnsColoringTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsColoringTypeDto> dto);

        Task Update (TblLnsColoringTypeDto dto);
        Task Update(List<TblLnsColoringTypeDto> dto);
        Task Add(TblLnsColoringTypeDto dto);
        Task Add(List<TblLnsColoringTypeDto> dto);

    }
}
