using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwCartableService
    {
        Task<IEnumerable<TblWfwCartableDto>> GetAll();
        Task<TblWfwCartableDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwCartableDto> dto);

        Task Update (TblWfwCartableDto dto);
        Task Update(List<TblWfwCartableDto> dto);
        Task Add(TblWfwCartableDto dto);
        Task Add(List<TblWfwCartableDto> dto);

    }
}
