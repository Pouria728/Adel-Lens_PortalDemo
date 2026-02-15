using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwConditionService
    {
        Task<IEnumerable<TblWfwConditionDto>> GetAll();
        Task<TblWfwConditionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwConditionDto> dto);

        Task Update (TblWfwConditionDto dto);
        Task Update(List<TblWfwConditionDto> dto);
        Task Add(TblWfwConditionDto dto);
        Task Add(List<TblWfwConditionDto> dto);

    }
}
