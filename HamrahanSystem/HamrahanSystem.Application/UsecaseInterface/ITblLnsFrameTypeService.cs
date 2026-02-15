using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsFrameTypeService
    {
        Task<IEnumerable<TblLnsFrameTypeDto>> GetAll();
        Task<(List<TblLnsFrameTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);

		Task<TblLnsFrameTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsFrameTypeDto> dto);

        Task Update (TblLnsFrameTypeDto dto);
        Task Update(List<TblLnsFrameTypeDto> dto);
        Task Add(TblLnsFrameTypeDto dto);
        Task Add(List<TblLnsFrameTypeDto> dto);

    }
}
