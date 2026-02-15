using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensTypeRLensIndexRService
    {
        Task<List<TblLnsLensTypeRLensIndexRDto>> GetAll();
		Task<(List<TblLnsLensTypeRLensIndexRDto>, int)> GetAll(int brandLensTypeRId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensTypeRLensIndexRDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensTypeRLensIndexRDto> dto);

        Task Update (TblLnsLensTypeRLensIndexRDto dto);
        Task Update(List<TblLnsLensTypeRLensIndexRDto> dto);
        Task Add(TblLnsLensTypeRLensIndexRDto dto);
        Task Add(List<TblLnsLensTypeRLensIndexRDto> dto);

    }
}
