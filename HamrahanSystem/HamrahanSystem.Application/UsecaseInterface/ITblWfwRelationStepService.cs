using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwRelationStepService
    {
        Task<IEnumerable<TblWfwRelationStepDto>> GetAll();
        Task<TblWfwRelationStepDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwRelationStepDto> dto);

        Task Update (TblWfwRelationStepDto dto);
        Task Update(List<TblWfwRelationStepDto> dto);
        Task Add(TblWfwRelationStepDto dto);
        Task Add(List<TblWfwRelationStepDto> dto);

    }
}
