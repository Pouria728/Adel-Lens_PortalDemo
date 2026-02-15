using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.UseCaseImplementation
{
    public class TblLnsDesignTypeService(ITblLnsDesignTypeRepository TblLnsDesignTypeRepository) :ITblLnsDesignTypeService
    {
        public Task Add(TblLnsDesignTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsDesignTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsDesignTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsDesignTypeDto>> GetAll()
        {
            var TblLnsDesignTypes=TblLnsDesignTypeRepository.GetAll();
            
            return TblLnsDesignTypeConverter.ToDtos(TblLnsDesignTypes);

        }
		public Task<(List<TblLnsDesignTypeDto>, int)> GetAll(int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{

			var tblLnsDesignType = TblLnsDesignTypeRepository.GetAll(maxResult, Page, rowInPage, sort, sidx);
			return Task.FromResult((TblLnsDesignTypeConverter.ToDtos(tblLnsDesignType.Result.Item1), tblLnsDesignType.Result.Item2));

		}

		public async Task<TblLnsDesignTypeDto> GetById(int id)
        {
            var TblLnsDesignType =TblLnsDesignTypeRepository.GetByKey(id);
            return TblLnsDesignTypeConverter.ToDto(TblLnsDesignType);
        }

        public Task Update(TblLnsDesignTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsDesignTypeRepository.Update(item);
        }

        public Task Update(List<TblLnsDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
