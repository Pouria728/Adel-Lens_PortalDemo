using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwStepActionService
    {
        Task<IEnumerable<TblWfwStepActionDto>> GetAll();
        Task<TblWfwStepActionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwStepActionDto> dto);

        Task Update (TblWfwStepActionDto dto);
        Task Update(List<TblWfwStepActionDto> dto);
        Task Add(TblWfwStepActionDto dto);
        Task Add(List<TblWfwStepActionDto> dto);

    }
}
