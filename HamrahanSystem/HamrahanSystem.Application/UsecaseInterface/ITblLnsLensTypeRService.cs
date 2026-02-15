using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensTypeRService
    {
        Task<IEnumerable<TblLnsLensTypeRDto>> GetAll();
        Task<(List<TblLnsLensTypeRDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);

		Task<TblLnsLensTypeRDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensTypeRDto> dto);

        Task Update (TblLnsLensTypeRDto dto);
        Task Update(List<TblLnsLensTypeRDto> dto);
        Task Add(TblLnsLensTypeRDto dto);
        Task Add(List<TblLnsLensTypeRDto> dto);

    }
}
