
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomLensIndexRepository : IRepository<TblLnsCustomLensIndex>
    {
        ICollection<TblLnsCustomLensIndex> GetAll();
        TblLnsCustomLensIndex GetByKey(int _CustomLensIndexId);
        Task<(List<TblLnsCustomLensIndex>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task Delete(int _ID);
        Task Delete(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices);

        Task Update(TblLnsCustomLensIndex tblLnsCustomLensIndex);
        Task Update(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices);
        Task Add(TblLnsCustomLensIndex tblLnsCustomLensIndex);
        Task Add(List<TblLnsCustomLensIndex> tblLnsCustomLensIndices);
    }
}
