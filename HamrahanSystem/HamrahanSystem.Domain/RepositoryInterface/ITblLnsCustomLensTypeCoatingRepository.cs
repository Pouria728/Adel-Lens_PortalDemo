
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomLensTypeCoatingRepository : IRepository<TblLnsCustomLensTypeCoating>
    {
        ICollection<TblLnsCustomLensTypeCoating> GetAll();
        TblLnsCustomLensTypeCoating GetByKey(int _CustomLensTypeCoatingId);
        Task<(List<TblLnsCustomLensTypeCoating>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task Delete(int _ID);
        Task Delete(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings);

        Task Update(TblLnsCustomLensTypeCoating tblLnsCustomLensTypeCoating);
        Task Update(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings);
        Task Add(TblLnsCustomLensTypeCoating tblLnsCustomLensTypeCoating);
        Task Add(List<TblLnsCustomLensTypeCoating> tblLnsCustomLensTypeCoatings);
    }
}

