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
    public class UserService(IUserRepository UserRepository) :IUserService
    {
        public Task Add(UserDto dto)
        {
			var item = dto.ToEntity();
			item.UserRoles= UserRoleConverter.ToEntities(dto.UserRoles);
			
			return UserRepository.Add(item);
		}

        public Task Add(List<UserDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<UserDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var Users= UserRepository.GetAll();
            
            return UserConverter.ToDtos(Users);

        }
		public Task<(List<UserDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var users = UserRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);

			return Task.FromResult((UserConverter.ToDtos(users.Result.Item1), users.Result.Item2));

		}

		public async Task<UserDto> GetById(int id)
        {
            var User = UserRepository.GetByKey(id);
            return UserConverter.ToDtoWithRelated(User,1);
        }
		public async Task<List<UserDto>> GetByUserName(string userName)
		{
			var User = UserRepository.GetByUserName(userName);
			return UserConverter.ToDtosWithRelated(User,5);
		}

		public Task Update(UserDto dto)
        {
			var item = dto.ToEntity();
			item.UserRoles = UserRoleConverter.ToEntities(dto.UserRoles);
			return UserRepository.Update(item);
		}

        public Task Update(List<UserDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
