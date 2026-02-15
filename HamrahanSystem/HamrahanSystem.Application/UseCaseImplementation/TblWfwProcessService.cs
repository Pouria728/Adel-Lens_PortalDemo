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
    public class TblWfwProcessService(ITblWfwProcessRepository tblWfwProcessRepository) :ITblWfwProcessService
    {
        public Task Add(TblWfwProcessDto dto)
        {
			var item = dto.ToEntity();
			return tblWfwProcessRepository.Add(item);
		}

        public Task Add(List<TblWfwProcessDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return tblWfwProcessRepository.Delete(id);
            
        }

        public Task Delete(List<TblWfwProcessDto> dto)
        {
            
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwProcessDto>> GetAll()
        {
            var TblWfwProcesss= tblWfwProcessRepository.GetAll();
            
            return TblWfwProcessConverter.ToDtos(TblWfwProcesss);

        }
		public Task<(List<TblWfwProcessDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var tblWfwProcess = tblWfwProcessRepository.GetAll(maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblWfwProcessConverter.ToDtos(tblWfwProcess.Result.Item1), tblWfwProcess.Result.Item2));

		}

		public async Task<TblWfwProcessDto> GetById(int id)
        {
            var TblWfwProcess = tblWfwProcessRepository.GetByKey(id);
            return TblWfwProcessConverter.ToDto(TblWfwProcess);
        }

        public Task Update(TblWfwProcessDto dto)
        {
			var item = dto.ToEntity();
			
			return tblWfwProcessRepository.Update(item);
		}

        public Task Update(List<TblWfwProcessDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
