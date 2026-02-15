using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwProcessActionService
    {
        Task<IEnumerable<TblWfwProcessActionDto>> GetAll();
        Task<TblWfwProcessActionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwProcessActionDto> dto);

        Task Update (TblWfwProcessActionDto dto);
        Task Update(List<TblWfwProcessActionDto> dto);
        Task Add(TblWfwProcessActionDto dto);
        Task Add(List<TblWfwProcessActionDto> dto);

    }
}
