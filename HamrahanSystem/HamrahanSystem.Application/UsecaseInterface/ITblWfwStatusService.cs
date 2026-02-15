using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwStatusService
    {
        Task<IEnumerable<TblWfwStatusDto>> GetAll();
        Task<TblWfwStatusDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwStatusDto> dto);

        Task Update (TblWfwStatusDto dto);
        Task Update(List<TblWfwStatusDto> dto);
        Task Add(TblWfwStatusDto dto);
        Task Add(List<TblWfwStatusDto> dto);

    }
}
