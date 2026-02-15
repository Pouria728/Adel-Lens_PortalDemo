using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IViewSalListCustomerService
    {
        Task<List<ViewSalListCustomerDto>> GetAll();
        Task<ViewSalListCustomerDto> GetById(int id);
        Task<List<ViewSalListCustomerDto>> GetByName(string name);

		Task Delete(int id);
        Task Delete(List<ViewSalListCustomerDto> dto);

        Task Update (ViewSalListCustomerDto dto);
        Task Update(List<ViewSalListCustomerDto> dto);
        Task Add(ViewSalListCustomerDto dto);
        Task Add(List<ViewSalListCustomerDto> dto);

    }
}
