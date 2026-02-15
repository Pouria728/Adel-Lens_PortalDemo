using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsCoatingService
    {
        Task<IEnumerable<TblLnsCoatingDto>> GetAll();

		Task<(List<TblLnsCoatingDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage,string sort, string sidx);
		Task<TblLnsCoatingDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsCoatingDto> dto);

        Task Update (TblLnsCoatingDto dto);
        Task Update(List<TblLnsCoatingDto> dto);
        Task Add(TblLnsCoatingDto dto);
        Task Add(List<TblLnsCoatingDto> dto);

    }
}
