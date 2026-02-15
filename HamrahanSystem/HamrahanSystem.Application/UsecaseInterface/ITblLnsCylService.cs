using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCylService
    {
        Task<IEnumerable<TblLnsCylDto>> GetAll();
		Task<(List<TblLnsCylDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsCylDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCylDto> dto);

        Task Update (TblLnsCylDto dto);
        Task Update(List<TblLnsCylDto> dto);
        Task Add(TblLnsCylDto dto);
        Task Add(List<TblLnsCylDto> dto);

    }
}
