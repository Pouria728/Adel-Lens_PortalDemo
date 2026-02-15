
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsDesignTypeLensIndexRepository : IRepository<TblLnsDesignTypeLensIndex>
    {
        ICollection<TblLnsDesignTypeLensIndex> GetAll();
        TblLnsDesignTypeLensIndex GetByKey(int _DesignTypeLensIndexId);
		Task<(List<TblLnsDesignTypeLensIndex>, int)> GetAll(int designTypeId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsDesignTypeLensIndex> tblLnsBrand);

		Task Update(TblLnsDesignTypeLensIndex tblLnsBrand);
		Task Update(List<TblLnsDesignTypeLensIndex> tblLnsBrand);
		Task Add(TblLnsDesignTypeLensIndex tblLnsBrand);
		Task Add(List<TblLnsDesignTypeLensIndex> tblLnsBrand);
	}
}
