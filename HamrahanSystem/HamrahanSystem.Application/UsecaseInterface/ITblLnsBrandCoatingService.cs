using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsBrandCoatingService
    {
        Task<IEnumerable<TblLnsBrandCoatingDto>> GetAll();
        Task<TblLnsBrandCoatingDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsBrandCoatingDto> dto);

        Task Update (TblLnsBrandCoatingDto dto);
        Task Update(List<TblLnsBrandCoatingDto> dto);
        Task Add(TblLnsBrandCoatingDto dto);
        Task Add(List<TblLnsBrandCoatingDto> dto);

    }
}
