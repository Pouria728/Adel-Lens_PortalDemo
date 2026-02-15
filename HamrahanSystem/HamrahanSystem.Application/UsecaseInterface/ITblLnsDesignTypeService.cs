using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsDesignTypeService
    {
        Task<IEnumerable<TblLnsDesignTypeDto>> GetAll();
		Task<(List<TblLnsDesignTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsDesignTypeDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsDesignTypeDto> dto);

        Task Update (TblLnsDesignTypeDto dto);
        Task Update(List<TblLnsDesignTypeDto> dto);
        Task Add(TblLnsDesignTypeDto dto);
        Task Add(List<TblLnsDesignTypeDto> dto);

    }
}
