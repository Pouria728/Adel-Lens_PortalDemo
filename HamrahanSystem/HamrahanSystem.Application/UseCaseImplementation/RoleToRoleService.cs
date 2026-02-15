using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class RoleToRoleService(IRoleToRoleRepository RoleToRoleRepository) :IRoleToRoleService
    {
        public Task Add(RoleToRoleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<RoleToRoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<RoleToRoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleToRoleDto>> GetAll()
        {
            var RoleToRoles=RoleToRoleRepository.GetAll();
            
            return RoleToRoleConverter.ToDtos(RoleToRoles);

        }

        public async Task<RoleToRoleDto> GetById(int id)
        {
            var RoleToRole = RoleToRoleRepository.GetByKey(id);
            return RoleToRoleConverter.ToDto(RoleToRole);
        }

        public Task Update(RoleToRoleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<RoleToRoleDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
