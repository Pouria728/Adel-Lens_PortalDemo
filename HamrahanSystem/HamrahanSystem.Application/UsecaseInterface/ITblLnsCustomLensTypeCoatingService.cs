using HamrahanSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCustomLensTypeCoatingService
    {
        Task<IEnumerable<TblLnsCustomLensTypeCoatingDto>> GetAll();
        Task<(List<TblLnsCustomLensTypeCoatingDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task<TblLnsCustomLensTypeCoatingDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCustomLensTypeCoatingDto> dto);
        Task Update(TblLnsCustomLensTypeCoatingDto dto);
        Task Update(List<TblLnsCustomLensTypeCoatingDto> dto);
        Task Add(TblLnsCustomLensTypeCoatingDto dto);
        Task Add(List<TblLnsCustomLensTypeCoatingDto> dto);
    }
}

