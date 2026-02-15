using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwRoleStepService
    {
        Task<IEnumerable<TblWfwRoleStepDto>> GetAll();
        Task<TblWfwRoleStepDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwRoleStepDto> dto);

        Task Update (TblWfwRoleStepDto dto);
        Task Update(List<TblWfwRoleStepDto> dto);
        Task Add(TblWfwRoleStepDto dto);
        Task Add(List<TblWfwRoleStepDto> dto);

    }
}
