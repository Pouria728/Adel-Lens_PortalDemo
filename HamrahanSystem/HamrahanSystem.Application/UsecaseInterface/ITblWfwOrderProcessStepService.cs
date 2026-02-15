using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblWfwOrderProcessStepService
	{
        Task<IEnumerable<TblWfwOrderProcessStepDto>> GetAll();
        Task<(List<TblWfwOrderProcessStepDto>,int)> GetAllByFilter(int? RequestStatusId, int? OrderStatusId, int? IndexDocument, int? CustomerId, int? RoleId, int? createdById, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string factorNo = "", string? fromDate = null, string? toDate = null);

		Task<TblWfwOrderProcessStepDto> GetById(long id);
        Task Delete(int id);
        Task Delete(List<TblWfwOrderProcessStepDto> dto);

        Task Update (TblWfwOrderProcessStepDto dto);
		Task UpdateStatus(TblWfwOrderProcessStepDto dto);
		Task Update(List<TblWfwOrderProcessStepDto> dto);
        Task Add(TblWfwOrderProcessStepDto dto);
        Task Add(List<TblWfwOrderProcessStepDto> dto);

    }
}
