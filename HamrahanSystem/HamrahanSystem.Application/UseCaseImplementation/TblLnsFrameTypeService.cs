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
    public class TblLnsFrameTypeService(ITblLnsFrameTypeRepository TblLnsFrameTypeRepository) :ITblLnsFrameTypeService
    {
        public Task Add(TblLnsFrameTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsFrameTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsFrameTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsFrameTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsFrameTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsFrameTypeDto>> GetAll()
        {
            var TblLnsFrameTypes= TblLnsFrameTypeRepository.GetAll();
            
            return TblLnsFrameTypeConverter.ToDtos(TblLnsFrameTypes);

        }

		public Task<(List<TblLnsFrameTypeDto>, int)> GetAll( int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			
				var tblLnsFrameTypes = TblLnsFrameTypeRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
				return Task.FromResult((TblLnsFrameTypeConverter.ToDtos(tblLnsFrameTypes.Result.Item1), tblLnsFrameTypes.Result.Item2));
			
		}

		public async Task<TblLnsFrameTypeDto> GetById(int id)
        {
            var TblLnsFrameType = TblLnsFrameTypeRepository.GetByKey(id);
            return TblLnsFrameTypeConverter.ToDto(TblLnsFrameType);
        }

        public Task Update(TblLnsFrameTypeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblLnsFrameTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
