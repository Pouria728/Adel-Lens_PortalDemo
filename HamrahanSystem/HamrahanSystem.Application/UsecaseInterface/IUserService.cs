using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
		Task<(List<UserDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx);
		Task<UserDto> GetById(int id);
		Task<List<UserDto>> GetByUserName(string userName);
		Task Delete(int id);
        Task Delete(List<UserDto> dto);

        Task Update (UserDto dto);
        Task Update(List<UserDto> dto);
        Task Add(UserDto dto);
        Task Add(List<UserDto> dto);

    }
}
