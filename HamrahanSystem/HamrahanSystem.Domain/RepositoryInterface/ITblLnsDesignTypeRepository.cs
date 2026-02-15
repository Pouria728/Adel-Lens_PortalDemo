
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsDesignTypeRepository : IRepository<TblLnsDesignType>
    {
        ICollection<TblLnsDesignType> GetAll();
        TblLnsDesignType GetByKey(int _DesignTypeId);
		Task<(List<TblLnsDesignType>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsDesignType> tblLnsLensTypeRs);

		Task Update(TblLnsDesignType tblLnsLensTypeR);
		Task Update(List<TblLnsDesignType> tblLnsLensTypeRs);
		Task Add(TblLnsDesignType tblLnsLensTypeR);
		Task Add(List<TblLnsDesignType> tblLnsLensTypeRs);
	}
}
