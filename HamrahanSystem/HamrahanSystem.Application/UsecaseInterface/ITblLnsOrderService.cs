using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsOrderService
    {
        Task<IEnumerable<TblLnsOrderDto>> GetAll();
        Task<(List<TblLnsOrderDto>,int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string search = "", string? fromDate = null, string? toDate = null);

		Task<TblLnsOrderDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsOrderDto> dto);

        Task Update (TblLnsOrderDto dto);
		Task UpdateStatus(int id,int statusOrder);
		Task Update(List<TblLnsOrderDto> dto);
        Task Add(TblLnsOrderDto dto,bool isSend);
        Task Add(List<TblLnsOrderDto> dto);

    }
}
