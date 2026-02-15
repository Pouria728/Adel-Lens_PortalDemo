
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
	public partial interface ITblWfwProcessRepository : IRepository<TblWfwProcess>
	{
		ICollection<TblWfwProcess> GetAll();
		IList<TblWfwProcess> GetFileter(int? indexDocumentId);
		TblWfwProcess GetByKey(int _ProcessId);
		Task<(List<TblWfwProcess>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblWfwProcess> tblWfwProcesses);

		Task Update(TblWfwProcess tblWfwProcess);
		Task Update(List<TblWfwProcess> tblWfwProcesses);
		Task Add(TblWfwProcess tblWfwProcess);
		Task Add(List<TblWfwProcess> tblWfwProcesses);
	}
}
