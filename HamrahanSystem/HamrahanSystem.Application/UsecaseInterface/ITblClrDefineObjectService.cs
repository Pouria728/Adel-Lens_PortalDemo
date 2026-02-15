using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblAccDefineCostCenterService
    {
        Task<IEnumerable<TblAccDefineCostCenterDto>> GetAll();
        Task<List<TblAccDefineCostCenterDto>> GetByName(string name);
		Task<TblAccDefineCostCenterDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblAccDefineCostCenterDto> dto);

        Task Update (TblAccDefineCostCenterDto dto);
        Task Update(List<TblAccDefineCostCenterDto> dto);
        Task Add(TblAccDefineCostCenterDto dto);
        Task Add(List<TblAccDefineCostCenterDto> dto);

    }
}
