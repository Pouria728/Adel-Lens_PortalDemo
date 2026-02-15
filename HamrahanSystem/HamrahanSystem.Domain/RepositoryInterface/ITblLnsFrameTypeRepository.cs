
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsFrameTypeRepository : IRepository<TblLnsFrameType>
    {
        ICollection<TblLnsFrameType> GetAll();
        TblLnsFrameType GetByKey(int _FrameTypeId);

		Task<(List<TblLnsFrameType>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsFrameType> tblLnsFrameTypes);

		Task Update(TblLnsFrameType tblLnsFrameType);
		Task Update(List<TblLnsFrameType> tblLnsFrameTypes);
		Task Add(TblLnsFrameType tblLnsFrameType);
		Task Add(List<TblLnsFrameType> tblLnsFrameTypes);
	}
}
