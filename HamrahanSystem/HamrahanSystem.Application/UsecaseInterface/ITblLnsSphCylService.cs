using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
	public interface ITblLnsSphCylService
	{
		Task<List<TblLnsSphCylDto>> GetAll();
		Task<(List<TblLnsSphCylDto>, int)> GetAll(int lensIndexRSphId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsSphCylDto> GetById(int id);
		Task Delete(int id);
		Task Delete(List<TblLnsSphCylDto> dto);

		Task Update(TblLnsSphCylDto dto);
		Task Update(List<TblLnsSphCylDto> dto);
		Task Add(TblLnsSphCylDto dto);
		Task Add(List<TblLnsSphCylDto> dto);

	}
}
