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
    public class TblWfwProcessStepService(ITblWfwProcessStepRepository TblWfwProcessStepRepository) :ITblWfwProcessStepService
    {
        public Task<int> Add(TblWfwProcessStepDto dto)
        {
			var item = dto.ToEntity();
			item.TblWfwRoleStepes = TblWfwRoleStepConverter.ToEntities(dto.TblWfwRoleStepes);
			return TblWfwProcessStepRepository.Add(item);
		}

        public Task Add(List<TblWfwProcessStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
			return TblWfwProcessStepRepository.Delete(id);
		}

        public Task Delete(List<TblWfwProcessStepDto> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblWfwProcessStepDto>> GetAll()
        {
            var TblWfwProcessSteps= TblWfwProcessStepRepository.GetAll();
            
            return TblWfwProcessStepConverter.ToDtos(TblWfwProcessSteps);

        }
		public Task<(List<TblWfwProcessStepDto>, int)> GetAll(int processId, int? maxResult, int? Page, int? rowInPage, string sort, string sidx)
		{
			var tblWfwProcessSteps = TblWfwProcessStepRepository.GetAll(processId,maxResult, Page, rowInPage,sort,sidx);

			return Task.FromResult((TblWfwProcessStepConverter.ToDtos(tblWfwProcessSteps.Result.Item1), tblWfwProcessSteps.Result.Item2));

		}

		public async Task<TblWfwProcessStepDto> GetById(int id)
        {
            var TblWfwProcessStep = TblWfwProcessStepRepository.GetByKey(id);
            return TblWfwProcessStepConverter.ToDtoWithRelated(TblWfwProcessStep,1);
        }

        public Task Update(TblWfwProcessStepDto dto)
        {
			var item = dto.ToEntity();
            item.TblWfwRoleStepes = TblWfwRoleStepConverter.ToEntities(dto.TblWfwRoleStepes);
			return TblWfwProcessStepRepository.Update(item);
		}

        public Task Update(List<TblWfwProcessStepDto> dto)
        {
            throw new NotImplementedException();
        }
    }
}
