using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleDto>> GetAll();
        Task<UserRoleDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<UserRoleDto> dto);

        Task Update (UserRoleDto dto);
        Task Update(List<UserRoleDto> dto);
        Task Add(UserRoleDto dto);
        Task Add(List<UserRoleDto> dto);

    }
}
