using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblInfoCustomerService
    {
        Task<IEnumerable<TblInfoCustomerDto>> GetAll();
        Task<TblInfoCustomerDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblInfoCustomerDto> dto);

        Task Update (TblInfoCustomerDto dto);
        Task Update(List<TblInfoCustomerDto> dto);
        Task Add(TblInfoCustomerDto dto);
        Task Add(List<TblInfoCustomerDto> dto);

    }
}
