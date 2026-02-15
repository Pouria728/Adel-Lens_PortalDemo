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
    public class TblAccDefineCostCenterService(ITblAccDefineCostCenterRepository TblAccDefineCostCenterRepository) :ITblAccDefineCostCenterService
    {
        public Task Add(TblAccDefineCostCenterDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Add(List<TblAccDefineCostCenterDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(List<TblAccDefineCostCenterDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblAccDefineCostCenterDto>> GetAll()
        {
            var TblAccDefineCostCenters= TblAccDefineCostCenterRepository.GetAll();
            
            return TblAccDefineCostCenterConverter.ToDtos(TblAccDefineCostCenters);

        }
		public async Task<List<TblAccDefineCostCenterDto>> GetByName(string name)
		{
			var TblAccDefineCostCenters = TblAccDefineCostCenterRepository.GetByName(name);

			return TblAccDefineCostCenterConverter.ToDtos(TblAccDefineCostCenters);

		}

		public async Task<TblAccDefineCostCenterDto> GetById(int id)
        {
            var TblAccDefineCostCenter = TblAccDefineCostCenterRepository.GetById(id);
            return TblAccDefineCostCenterConverter.ToDto(TblAccDefineCostCenter);
        }

        public Task Update(TblAccDefineCostCenterDto dto)
        {
            throw new NotImplementedException();
        }

        public Task Update(List<TblAccDefineCostCenterDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
