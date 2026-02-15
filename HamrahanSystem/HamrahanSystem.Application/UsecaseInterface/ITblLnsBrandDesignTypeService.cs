using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsBrandDesignTypeService
    {
        Task<IEnumerable<TblLnsBrandDesignTypeDto>> GetAll();
		Task<(List<TblLnsBrandDesignTypeDto>, int)> GetAll(int brandLensTypeId,int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsBrandDesignTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsBrandDesignTypeDto> dto);

        Task Update (TblLnsBrandDesignTypeDto dto);
        Task Update(List<TblLnsBrandDesignTypeDto> dto);
        Task Add(TblLnsBrandDesignTypeDto dto);
        Task Add(List<TblLnsBrandDesignTypeDto> dto);

    }
}
