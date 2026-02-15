using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IBaseRequestStepService
    {
        Task<IEnumerable<BaseRequestStepDto>> GetAll();
		Task<List<BaseRequestStepDto>> GetAllActive();
		Task<(List<BaseRequestStepDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage,string sort, string sidx);
		Task<BaseRequestStepDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<BaseRequestStepDto> dto);

        Task Update (BaseRequestStepDto dto);
        Task Update(List<BaseRequestStepDto> dto);
        Task Add(BaseRequestStepDto dto);
        Task Add(List<BaseRequestStepDto> dto);

    }
}
