using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwProcessService
    {
        Task<IEnumerable<TblWfwProcessDto>> GetAll();
		Task<(List<TblWfwProcessDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblWfwProcessDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwProcessDto> dto);

        Task Update (TblWfwProcessDto dto);
        Task Update(List<TblWfwProcessDto> dto);
        Task Add(TblWfwProcessDto dto);
        Task Add(List<TblWfwProcessDto> dto);

    }
}
