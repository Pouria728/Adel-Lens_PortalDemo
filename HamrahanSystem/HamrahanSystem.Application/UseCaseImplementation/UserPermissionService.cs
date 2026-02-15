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
    public class UserPermissionService(IUserPermissionRepository UserPermissionRepository) :IUserPermissionService
    {
        public Task Add(UserPermissionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<UserPermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<UserPermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserPermissionDto>> GetAll()
        {
            var UserPermissions= UserPermissionRepository.GetAll();
            
            return UserPermissionConverter.ToDtos(UserPermissions);

        }

        public async Task<UserPermissionDto> GetById(int id)
        {
            var UserPermission = UserPermissionRepository.GetByKey(id);
            return UserPermissionConverter.ToDto(UserPermission);
        }

        public Task Update(UserPermissionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<UserPermissionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
