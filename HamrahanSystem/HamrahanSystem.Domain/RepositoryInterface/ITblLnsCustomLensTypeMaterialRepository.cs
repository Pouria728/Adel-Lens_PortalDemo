
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomLensTypeMaterialRepository : IRepository<TblLnsCustomLensTypeMaterial>
    {
        ICollection<TblLnsCustomLensTypeMaterial> GetAll();
        TblLnsCustomLensTypeMaterial GetByKey(int _CustomLensTypeMaterialId);
        Task<(List<TblLnsCustomLensTypeMaterial>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task Delete(int _ID);
        Task Delete(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials);

        Task Update(TblLnsCustomLensTypeMaterial tblLnsCustomLensTypeMaterial);
        Task Update(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials);
        Task Add(TblLnsCustomLensTypeMaterial tblLnsCustomLensTypeMaterial);
        Task Add(List<TblLnsCustomLensTypeMaterial> tblLnsCustomLensTypeMaterials);
    }
}
