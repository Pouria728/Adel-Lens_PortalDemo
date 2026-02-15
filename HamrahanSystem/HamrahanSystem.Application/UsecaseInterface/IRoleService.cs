using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAll();
		Task<(List<RoleDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<RoleDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<RoleDto> dto);

        Task Update (RoleDto dto);
        Task Update(List<RoleDto> dto);
        Task Add(RoleDto dto);
        Task Add(List<RoleDto> dto);

    }
}
