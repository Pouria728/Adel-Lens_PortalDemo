using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensTypeService
    {
        Task<IEnumerable<TblLnsLensTypeDto>> GetAll();
		Task<(List<TblLnsLensTypeDto>, int)> GetAll( int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensTypeDto> dto);

        Task Update (TblLnsLensTypeDto dto);
        Task Update(List<TblLnsLensTypeDto> dto);
        Task Add(TblLnsLensTypeDto dto);
        Task Add(List<TblLnsLensTypeDto> dto);

    }
}
