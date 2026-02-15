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
    public class PermissionService(IPermissionRepository PermissionRepository) :IPermissionService
    {
        public Task Add(PermissionDto dto)
        {
			throw new NotImplementedException();
		}

        public Task Add(List<PermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<PermissionDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PermissionDto>> GetAll()
        {
            var Permissions=PermissionRepository.GetAll();
            
            return PermissionConverter.ToDtos(Permissions);

        }
	

		public async Task<PermissionDto> GetById(int id)
        {
            var permission = PermissionRepository.GetByKey(id);
            return PermissionConverter.ToDto(permission);
        }

        public Task Update(PermissionDto dto)
        {
			throw new NotImplementedException();
		}

        public Task Update(List<PermissionDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
