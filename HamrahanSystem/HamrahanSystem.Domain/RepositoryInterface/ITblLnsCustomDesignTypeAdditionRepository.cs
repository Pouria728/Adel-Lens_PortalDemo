
using System;
using System.Collections.Generic;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Domain.Repository
{
    public partial interface ITblLnsCustomDesignTypeAdditionRepository : IRepository<TblLnsCustomDesignTypeAddition>
    {
        ICollection<TblLnsCustomDesignTypeAddition> GetAll();
        TblLnsCustomDesignTypeAddition GetByKey(int _CustomDesignTypeAdditionId);
        Task<(List<TblLnsCustomDesignTypeAddition>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
        Task Delete(int _ID);
        Task Delete(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions);

        Task Update(TblLnsCustomDesignTypeAddition tblLnsCustomDesignTypeAddition);
        Task Update(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions);
        Task Add(TblLnsCustomDesignTypeAddition tblLnsCustomDesignTypeAddition);
        Task Add(List<TblLnsCustomDesignTypeAddition> tblLnsCustomDesignTypeAdditions);
    }
}
