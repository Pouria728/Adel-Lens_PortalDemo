using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IBaseRequesteService
    {
        Task<List<BaseRequesteDto>> GetAll();
		Task<List<BaseRequesteDto>> GetAllActive();
		Task<(List<BaseRequesteDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage,string sort, string sidx);
		Task<BaseRequesteDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<BaseRequesteDto> dto);

        Task Update (BaseRequesteDto dto);
        Task Update(List<BaseRequesteDto> dto);
        Task Add(BaseRequesteDto dto);
        Task Add(List<BaseRequesteDto> dto);

    }
}
