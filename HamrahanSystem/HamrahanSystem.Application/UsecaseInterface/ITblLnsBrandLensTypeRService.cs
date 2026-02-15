using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsBrandLensTypeRService
    {
        Task<List<TblLnsBrandLensTypeRDto>> GetAll();
		Task<(List<TblLnsBrandLensTypeRDto>, int)> GetAll(int brandId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsBrandLensTypeRDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsBrandLensTypeRDto> dto);

        Task Update (TblLnsBrandLensTypeRDto dto);
        Task Update(List<TblLnsBrandLensTypeRDto> dto);
        Task Add(TblLnsBrandLensTypeRDto dto);
        Task Add(List<TblLnsBrandLensTypeRDto> dto);

    }
}
