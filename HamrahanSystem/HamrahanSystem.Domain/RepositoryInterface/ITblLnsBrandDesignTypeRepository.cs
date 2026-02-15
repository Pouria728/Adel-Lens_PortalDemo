
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsBrandDesignTypeRepository : IRepository<TblLnsBrandDesignType>
    {
        ICollection<TblLnsBrandDesignType> GetAll();
        TblLnsBrandDesignType GetByKey(int _BrandDesignTypeId);
		Task<(List<TblLnsBrandDesignType>, int)> GetAll(int brandLensTypeId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes);

		Task Update(TblLnsBrandDesignType tblLnsBrandDesignType);
		Task Update(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes);
		Task Add(TblLnsBrandDesignType tblLnsBrandDesignType);
		Task Add(List<TblLnsBrandDesignType> tblLnsBrandDesignTypes);
	}
}
