using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwAttachService
    {
        Task<IEnumerable<TblWfwAttachDto>> GetAll();
        Task<TblWfwAttachDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwAttachDto> dto);

        Task Update (TblWfwAttachDto dto);
        Task Update(List<TblWfwAttachDto> dto);
        Task Add(TblWfwAttachDto dto);
        Task Add(List<TblWfwAttachDto> dto);

    }
}
