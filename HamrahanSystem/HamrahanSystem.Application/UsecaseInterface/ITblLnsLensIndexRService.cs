using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensIndexRService
    {
        Task<IEnumerable<TblLnsLensIndexRDto>> GetAll();
		Task<(List<TblLnsLensIndexRDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensIndexRDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensIndexRDto> dto);

        Task Update (TblLnsLensIndexRDto dto);
        Task Update(List<TblLnsLensIndexRDto> dto);
        Task Add(TblLnsLensIndexRDto dto);
        Task Add(List<TblLnsLensIndexRDto> dto);

    }
}
