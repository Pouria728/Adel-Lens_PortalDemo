using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblLnsDesignTypeLensIndexService
    {
        Task<IEnumerable<TblLnsDesignTypeLensIndexDto>> GetAll();
		Task<(List<TblLnsDesignTypeLensIndexDto>, int)> GetAll(int designTypeId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<TblLnsDesignTypeLensIndexDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblLnsDesignTypeLensIndexDto> dto);

        Task Update (TblLnsDesignTypeLensIndexDto dto);
        Task Update(List<TblLnsDesignTypeLensIndexDto> dto);
        Task Add(TblLnsDesignTypeLensIndexDto dto);
        Task Add(List<TblLnsDesignTypeLensIndexDto> dto);

    }
}
