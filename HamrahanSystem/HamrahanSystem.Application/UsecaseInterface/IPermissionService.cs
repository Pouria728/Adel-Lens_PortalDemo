using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAll();
		Task<PermissionDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<PermissionDto> dto);

        Task Update (PermissionDto dto);
        Task Update(List<PermissionDto> dto);
        Task Add(PermissionDto dto);
        Task Add(List<PermissionDto> dto);

    }
}
