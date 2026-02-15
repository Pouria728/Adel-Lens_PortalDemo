
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsLensIndexMaterialTypeRepository : IRepository<TblLnsLensIndexMaterialType>
    {
        ICollection<TblLnsLensIndexMaterialType> GetAll();
        TblLnsLensIndexMaterialType GetByKey(int _LensIndexMaterialTypeId);
		Task<(List<TblLnsLensIndexMaterialType>, int)> GetAll(int designTypeLensIndexId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task Delete(int _ID);
		Task Delete(List<TblLnsLensIndexMaterialType> tblLnsLensIndexMaterialTypes);

		Task Update(TblLnsLensIndexMaterialType tblLnsLensIndexMaterialType);
		Task Update(List<TblLnsLensIndexMaterialType> tblLnsLensIndexMaterialTypes);
		Task Add(TblLnsLensIndexMaterialType tblLnsLensIndexMaterial);
		Task Add(List<TblLnsLensIndexMaterialType> tblLnsLensIndexMaterials);
	}
}
