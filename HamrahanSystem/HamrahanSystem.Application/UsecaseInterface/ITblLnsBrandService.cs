using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsBrandService
    {
        Task<IEnumerable<TblLnsBrandDto>> GetAll();
		Task<List<TblLnsBrandDto>> GetAllActive();
		Task<(List<TblLnsBrandDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsBrandDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsBrandDto> dto);

        Task Update (TblLnsBrandDto dto);
        Task Update(List<TblLnsBrandDto> dto);
        Task Add(TblLnsBrandDto dto);
        Task Add(List<TblLnsBrandDto> dto);

    }
}
