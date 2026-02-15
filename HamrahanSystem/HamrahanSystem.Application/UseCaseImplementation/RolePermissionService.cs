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
    public class RolePermissionService(IRolePermissionRepository RolePermissionRepository) : IRolePermissionService
	{
        public Task Add(RolePermissionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<RolePermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<RolePermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RolePermissionDto>> GetAll()
        {
            var banks= RolePermissionRepository.GetAll();
            
            return RolePermissionConverter.ToDtos(banks);

        }

        public async Task<RolePermissionDto> GetById(int id)
        {
            var bank = RolePermissionRepository.GetByKey(id);
            return RolePermissionConverter.ToDto(bank);
        }

        public Task Update(RolePermissionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<RolePermissionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
