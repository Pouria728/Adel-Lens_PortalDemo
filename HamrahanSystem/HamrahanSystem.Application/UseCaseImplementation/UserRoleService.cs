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
    public class UserRoleService(IUserRoleRepository UserRoleRepository) :IUserRoleService
    {
        public Task Add(UserRoleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<UserRoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<UserRoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRoleDto>> GetAll()
        {
            var UserRoles= UserRoleRepository.GetAll();
            
            return UserRoleConverter.ToDtos(UserRoles);

        }

        public async Task<UserRoleDto> GetById(int id)
        {
            var UserRole = UserRoleRepository.GetByKey(id);
            return UserRoleConverter.ToDto(UserRole);
        }

        public Task Update(UserRoleDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<UserRoleDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
