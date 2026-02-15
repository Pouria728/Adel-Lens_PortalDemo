using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsOrderserviceService
    {
        Task<IEnumerable<TblLnsOrderserviceDto>> GetAll();
        Task<TblLnsOrderserviceDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsOrderserviceDto> dto);

        Task Update (TblLnsOrderserviceDto dto);
        Task Update(List<TblLnsOrderserviceDto> dto);
        Task Add(TblLnsOrderserviceDto dto);
        Task Add(List<TblLnsOrderserviceDto> dto);

    }
}
