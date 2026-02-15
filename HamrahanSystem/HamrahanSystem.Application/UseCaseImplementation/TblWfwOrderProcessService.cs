using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblWfwOrderProcessService(ITblWfwOrderProcessRepository TblWfwOrderProcessRepository) :ITblWfwOrderProcessService
    {
        public Task Add(TblWfwOrderProcessDto dto)
        {
			var item = dto.ToEntity();
			return TblWfwOrderProcessRepository.Add(item);
		}

        public Task Add(List<TblWfwOrderProcessDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblWfwOrderProcessDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwOrderProcessDto>> GetAll()
        {
            var TblWfwOrderProcesss=TblWfwOrderProcessRepository.GetAll();
            
            return TblWfwOrderProcessConverter.ToDtos(TblWfwOrderProcesss);

        }
		public Task<(List<TblWfwOrderProcessDto>,int)> GetAllByFilter(int? OrderStatusId,int? IndexDocument,int? CustomerId,int? UserId, int? maxResult, int? page, int? rowInPage, string sort, string sidx)
		{
			var TblWfwOrderProcesss = TblWfwOrderProcessRepository.GetAllByFilter(OrderStatusId, IndexDocument, CustomerId,UserId, maxResult, page, rowInPage,sort,sidx);
			return Task.FromResult((TblWfwOrderProcessConverter.ToDtosWithRelated(TblWfwOrderProcesss.Result.Item1,1), TblWfwOrderProcesss.Result.Item2));

		}

		public async Task<TblWfwOrderProcessDto> GetById(int id)
        {
            var TblWfwOrderProcess =TblWfwOrderProcessRepository.GetByKey(id);
            return TblWfwOrderProcessConverter.ToDto(TblWfwOrderProcess);
        }

        public Task Update(TblWfwOrderProcessDto dto)
        {
			var item = dto.ToEntity();
			return TblWfwOrderProcessRepository.Update(item);
		}

        public Task Update(List<TblWfwOrderProcessDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
