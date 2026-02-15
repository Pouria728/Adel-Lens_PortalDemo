using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.DTOs;

namespace HamrahanSystem.Application.UseCaseInterface
{
    public interface ITblClrDefineObjectService
    {
        Task<IEnumerable<TblClrDefineObjectDto>> GetAll();
        Task<List<TblClrDefineObjectDto>> GetByName(string name);
        Task<List<TblClrDefineObjectDto>> Search(string term);
		Task<TblClrDefineObjectDto> GetById(int id);
        Task Delete(int id);
        Task Delete(List<TblClrDefineObjectDto> dto);

        Task Update (TblClrDefineObjectDto dto);
        Task Update(List<TblClrDefineObjectDto> dto);
        Task Add(TblClrDefineObjectDto dto);
        Task Add(List<TblClrDefineObjectDto> dto);

    }
}
