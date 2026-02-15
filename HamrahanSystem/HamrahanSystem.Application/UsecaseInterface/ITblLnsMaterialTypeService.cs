using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsMaterialTypeService
    {
        Task<IEnumerable<TblLnsMaterialTypeDto>> GetAll();
		Task<(List<TblLnsMaterialTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsMaterialTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsMaterialTypeDto> dto);

        Task Update (TblLnsMaterialTypeDto dto);
        Task Update(List<TblLnsMaterialTypeDto> dto);
        Task Add(TblLnsMaterialTypeDto dto);
        Task Add(List<TblLnsMaterialTypeDto> dto);

    }
}
