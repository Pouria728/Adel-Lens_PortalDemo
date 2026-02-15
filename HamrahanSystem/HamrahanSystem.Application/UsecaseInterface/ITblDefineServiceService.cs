using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblDefineServiceService
    {
        Task<IEnumerable<TblDefineServiceDto>> GetAll();
        Task<TblDefineServiceDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblDefineServiceDto> dto);

        Task Update (TblDefineServiceDto dto);
        Task Update(List<TblDefineServiceDto> dto);
        Task Add(TblDefineServiceDto dto);
        Task Add(List<TblDefineServiceDto> dto);

    }
}
