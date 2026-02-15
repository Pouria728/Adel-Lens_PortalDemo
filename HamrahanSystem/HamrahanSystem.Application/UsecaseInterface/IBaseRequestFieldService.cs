using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IBaseRequestFieldService
    {
        Task<IEnumerable<BaseRequestFieldDto>> GetAll();
		Task<List<BaseRequestFieldDto>> GetAllActive();
		Task<(List<BaseRequestFieldDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<BaseRequestFieldDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<BaseRequestFieldDto> dto);

        Task Update (BaseRequestFieldDto dto);
        Task Update(List<BaseRequestFieldDto> dto);
        Task Add(BaseRequestFieldDto dto);
        Task Add(List<BaseRequestFieldDto> dto);

    }
}
