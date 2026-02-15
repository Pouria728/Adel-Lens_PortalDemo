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
    public class TblClrDefineObjectService(ITblClrDefineObjectRepository TblClrDefineObjectRepository) :ITblClrDefineObjectService
    {
        public Task Add(TblClrDefineObjectDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblClrDefineObjectDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblClrDefineObjectDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblClrDefineObjectDto>> GetAll()
        {
            var TblClrDefineObjects= TblClrDefineObjectRepository.GetAll();
            
            return TblClrDefineObjectConverter.ToDtos(TblClrDefineObjects);

        }
		public async Task<List<TblClrDefineObjectDto>> GetByName(string name)
		{
			var TblClrDefineObjects = TblClrDefineObjectRepository.GetByName(name);

			return TblClrDefineObjectConverter.ToDtos(TblClrDefineObjects);

		}
		public async Task<List<TblClrDefineObjectDto>> Search(string term)
		{
			var TblClrDefineObjects = TblClrDefineObjectRepository.Search(term);
			return TblClrDefineObjectConverter.ToDtos(TblClrDefineObjects);
		}

		public async Task<TblClrDefineObjectDto> GetById(int id)
        {
            var TblClrDefineObject = TblClrDefineObjectRepository.GetById(id);
            return TblClrDefineObjectConverter.ToDto(TblClrDefineObject);
        }

        public Task Update(TblClrDefineObjectDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblClrDefineObjectDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
