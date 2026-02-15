using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsBrandLensTypeService
    {
        Task<List<TblLnsBrandLensTypeDto>> GetAll();
        Task<(List<TblLnsBrandLensTypeDto>, int)> GetAll(int brandId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsBrandLensTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsBrandLensTypeDto> dto);

        Task Update (TblLnsBrandLensTypeDto dto);
        Task Update(List<TblLnsBrandLensTypeDto> dto);
        Task Add(TblLnsBrandLensTypeDto dto);
        Task Add(List<TblLnsBrandLensTypeDto> dto);

    }
}
