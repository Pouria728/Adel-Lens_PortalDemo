using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<UserPermissionDto>> GetAll();
        Task<UserPermissionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<UserPermissionDto> dto);

        Task Update (UserPermissionDto dto);
        Task Update(List<UserPermissionDto> dto);
        Task Add(UserPermissionDto dto);
        Task Add(List<UserPermissionDto> dto);

    }
}
