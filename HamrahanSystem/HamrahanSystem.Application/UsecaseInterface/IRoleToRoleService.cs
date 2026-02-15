using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IRoleToRoleService
    {
        Task<IEnumerable<RoleToRoleDto>> GetAll();
        Task<RoleToRoleDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<RoleToRoleDto> dto);

        Task Update (RoleToRoleDto dto);
        Task Update(List<RoleToRoleDto> dto);
        Task Add(RoleToRoleDto dto);
        Task Add(List<RoleToRoleDto> dto);

    }
}
