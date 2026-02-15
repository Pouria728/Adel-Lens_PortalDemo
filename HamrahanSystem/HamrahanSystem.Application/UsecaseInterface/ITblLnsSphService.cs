using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsSphService
    {
        Task<IEnumerable<TblLnsSphDto>> GetAll();
		Task<(List<TblLnsSphDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsSphDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsSphDto> dto);

        Task Update (TblLnsSphDto dto);
        Task Update(List<TblLnsSphDto> dto);
        Task Add(TblLnsSphDto dto);
        Task Add(List<TblLnsSphDto> dto);

    }
}
