using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwOrderProcessService
	{
        Task<IEnumerable<TblWfwOrderProcessDto>> GetAll();
        Task<(List<TblWfwOrderProcessDto>,int)> GetAllByFilter(int? OrderStatusId, int? IndexDocument, int? CustomerId, int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx);

		Task<TblWfwOrderProcessDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblWfwOrderProcessDto> dto);

        Task Update (TblWfwOrderProcessDto dto);
        Task Update(List<TblWfwOrderProcessDto> dto);
        Task Add(TblWfwOrderProcessDto dto);
        Task Add(List<TblWfwOrderProcessDto> dto);

    }
}
