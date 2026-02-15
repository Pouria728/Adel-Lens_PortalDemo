
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsBrandCoatingRepository : IRepository<TblLnsBrandCoating>
    {
        ICollection<TblLnsBrandCoating> GetAll();
        TblLnsBrandCoating GetByKey(int _BrandCoatingId);
    }
}
