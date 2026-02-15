using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermissionDto>> GetAll();
        Task<RolePermissionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<RolePermissionDto> dto);

        Task Update (RolePermissionDto dto);
        Task Update(List<RolePermissionDto> dto);
        Task Add(RolePermissionDto dto);
        Task Add(List<RolePermissionDto> dto);

    }
}
