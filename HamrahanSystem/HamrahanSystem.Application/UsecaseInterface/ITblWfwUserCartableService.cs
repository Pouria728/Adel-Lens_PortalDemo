using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwUserCartableService
    {
        Task<IEnumerable<TblWfwUserCartableDto>> GetAll();
        Task<TblWfwUserCartableDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwUserCartableDto> dto);

        Task Update (TblWfwUserCartableDto dto);
        Task Update(List<TblWfwUserCartableDto> dto);
        Task Add(TblWfwUserCartableDto dto);
        Task Add(List<TblWfwUserCartableDto> dto);

    }
}
