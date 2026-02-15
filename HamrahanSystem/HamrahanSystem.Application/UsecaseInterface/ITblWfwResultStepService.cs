using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwResultStepService
    {
        Task<IEnumerable<TblWfwResultStepDto>> GetAll();
        Task<TblWfwResultStepDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwResultStepDto> dto);

        Task Update (TblWfwResultStepDto dto);
        Task Update(List<TblWfwResultStepDto> dto);
        Task Add(TblWfwResultStepDto dto);
        Task Add(List<TblWfwResultStepDto> dto);

    }
}
