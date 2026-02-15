using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwRelationStepConditionService
    {
        Task<IEnumerable<TblWfwRelationStepConditionDto>> GetAll();
        Task<TblWfwRelationStepConditionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwRelationStepConditionDto> dto);

        Task Update (TblWfwRelationStepConditionDto dto);
        Task Update(List<TblWfwRelationStepConditionDto> dto);
        Task Add(TblWfwRelationStepConditionDto dto);
        Task Add(List<TblWfwRelationStepConditionDto> dto);

    }
}
