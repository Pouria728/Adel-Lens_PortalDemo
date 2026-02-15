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
    public class RoleService(IRoleRepository RoleRepository) :IRoleService
    {
        public Task Add(RoleDto dto)
        {
			var item = dto.ToEntity();
			item.RolePermissions = RolePermissionConverter.ToEntities(dto.RolePermissions);
			return RoleRepository.Add(item);
		}

        public Task Add(List<RoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return RoleRepository.Delete(id);
        }

        public Task Delete(List<RoleDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RoleDto>> GetAll()
        {
            var Roles=RoleRepository.GetAll().ToList();
            
            return RoleConverter.ToDtos(Roles);

        }
		public Task<(List<RoleDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var roles = RoleRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((RoleConverter.ToDtos(roles.Result.Item1), roles.Result.Item2));

		}

		public async Task<RoleDto> GetById(int id)
        {
            var Role = RoleRepository.GetByKey(id);
            return RoleConverter.ToDtoWithRelated(Role,1);
        }

        public Task Update(RoleDto dto)
        {
			var item = dto.ToEntity();
            item.RolePermissions = RolePermissionConverter.ToEntities(dto.RolePermissions);
			return RoleRepository.Update(item);
		}

        public Task Update(List<RoleDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
