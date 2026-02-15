using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
	public interface ITblWfwProcessStepService
	{
		Task<IEnumerable<TblWfwProcessStepDto>> GetAll();
		Task<(List<TblWfwProcessStepDto>, int)> GetAll(int processId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblWfwProcessStepDto> GetById(int id);
		Task Delete(int id);
		Task Delete(List<TblWfwProcessStepDto> dto);

		Task Update(TblWfwProcessStepDto dto);
		Task Update(List<TblWfwProcessStepDto> dto);
		Task<int> Add(TblWfwProcessStepDto dto);
		Task Add(List<TblWfwProcessStepDto> dto);

	}
}
