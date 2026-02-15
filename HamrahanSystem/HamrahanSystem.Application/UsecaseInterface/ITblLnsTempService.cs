using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsTempService
    {
        Task<IEnumerable<TblLnsTempDto>> GetAll();
        Task<TblLnsTempDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsTempDto> dto);

        Task Update (TblLnsTempDto dto);
        Task Update(List<TblLnsTempDto> dto);
        Task Add(TblLnsTempDto dto);
        Task Add(List<TblLnsTempDto> dto);

    }
}
