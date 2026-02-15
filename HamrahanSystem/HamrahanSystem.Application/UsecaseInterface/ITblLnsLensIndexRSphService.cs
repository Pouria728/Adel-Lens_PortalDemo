using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsLensIndexRSphService
    {
        Task<List<TblLnsLensIndexRSphDto>> GetAll();
		Task<(List<TblLnsLensIndexRSphDto>, int)> GetAll(int lensTypeRLensIndexRId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsLensIndexRSphDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsLensIndexRSphDto> dto);

        Task Update (TblLnsLensIndexRSphDto dto);
        Task Update(List<TblLnsLensIndexRSphDto> dto);
        Task Add(TblLnsLensIndexRSphDto dto);
        Task Add(List<TblLnsLensIndexRSphDto> dto);

    }
}
