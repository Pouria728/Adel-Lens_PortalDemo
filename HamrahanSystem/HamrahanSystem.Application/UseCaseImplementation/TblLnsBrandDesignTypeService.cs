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
    public class TblLnsBrandDesignTypeService(ITblLnsBrandDesignTypeRepository TblLnsBrandDesignTypeRepository) :ITblLnsBrandDesignTypeService
    {
        public Task Add(TblLnsBrandDesignTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsBrandDesignTypeRepository.Add(item);
		}

        public Task Add(List<TblLnsBrandDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            return TblLnsBrandDesignTypeRepository.Delete(id);
        }

        public Task Delete(List<TblLnsBrandDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblLnsBrandDesignTypeDto>> GetAll()
        {
            var TblLnsBrandDesignTypes= TblLnsBrandDesignTypeRepository.GetAll();
            
            return TblLnsBrandDesignTypeConverter.ToDtos(TblLnsBrandDesignTypes);

        }
		public Task<(List<TblLnsBrandDesignTypeDto>, int)> GetAll(int brandLensTypeId, int? maxResult, int? Page, int? rowInPage,string sort, string sidx)
		{
			var TblLnsBrands = TblLnsBrandDesignTypeRepository.GetAll(brandLensTypeId,maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblLnsBrandDesignTypeConverter.ToDtosWithRelated(TblLnsBrands.Result.Item1,2), TblLnsBrands.Result.Item2));

		}


		public async Task<TblLnsBrandDesignTypeDto> GetById(int id)
        {
            var TblLnsBrandDesignType = TblLnsBrandDesignTypeRepository.GetByKey(id);
            return TblLnsBrandDesignTypeConverter.ToDtoWithRelated(TblLnsBrandDesignType,2);
        }

        public Task Update(TblLnsBrandDesignTypeDto dto)
        {
			var item = dto.ToEntity();
			return TblLnsBrandDesignTypeRepository.Update(item);
		}

        public Task Update(List<TblLnsBrandDesignTypeDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
