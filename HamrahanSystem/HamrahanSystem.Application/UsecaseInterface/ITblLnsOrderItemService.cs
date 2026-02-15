using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsOrderItemService
    {
        Task<IEnumerable<TblLnsOrderItemDto>> GetAll();
        Task<TblLnsOrderItemDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsOrderItemDto> dto);

        Task Update (TblLnsOrderItemDto dto);
        Task Update(List<TblLnsOrderItemDto> dto);
        Task UpdateProvidedQuantity(List<TblLnsOrderItemDto> dto);

		Task Add(TblLnsOrderItemDto dto);
        Task Add(List<TblLnsOrderItemDto> dto);

    }
}
