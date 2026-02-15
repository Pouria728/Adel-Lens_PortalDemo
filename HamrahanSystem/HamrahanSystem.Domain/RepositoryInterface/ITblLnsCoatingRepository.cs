
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCoatingRepository : IRepository<TblLnsCoating>
    {
        ICollection<TblLnsCoating> GetAll();
        TblLnsCoating GetByKey(int _CoatingId);
		Task<(List<TblLnsCoating>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsCoating> tblLnsCoatings);

		Task Update(TblLnsCoating tblLnsCoating);
		Task Update(List<TblLnsCoating> tblLnsCoatings);
		Task Add(TblLnsCoating tblLnsCoating);
		Task Add(List<TblLnsCoating> tblLnsCoatings);
	}
}
