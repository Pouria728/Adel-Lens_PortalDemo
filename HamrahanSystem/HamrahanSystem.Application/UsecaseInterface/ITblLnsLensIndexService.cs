using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensIndexService
    {
        Task<IEnumerable<TblLnsLensIndexDto>> GetAll();
		Task<(List<TblLnsLensIndexDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensIndexDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensIndexDto> dto);

        Task Update (TblLnsLensIndexDto dto);
        Task Update(List<TblLnsLensIndexDto> dto);
        Task Add(TblLnsLensIndexDto dto);
        Task Add(List<TblLnsLensIndexDto> dto);

    }
}
