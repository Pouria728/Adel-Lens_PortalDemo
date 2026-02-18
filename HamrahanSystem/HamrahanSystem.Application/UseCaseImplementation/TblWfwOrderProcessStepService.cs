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
	public class TblWfwOrderProcessStepService(
		ITblWfwOrderProcessStepRepository TblWfwOrderProcessStepRepository,
		ITblWfwOrderProcessRepository tblWfwOrderProcessRepository,
		ITblWfwProcessStepRepository tblWfwProcessStepRepository,
		ITblWfwRelationStepRepository tblWfwRelationStepRepository) : ITblWfwOrderProcessStepService
	{
		public Task Add(TblWfwOrderProcessStepDto dto)
		{
			var item = dto.ToEntity();
			return TblWfwOrderProcessStepRepository.Add(item);
		}

		public Task Add(List<TblWfwOrderProcessStepDto> dto)
		{
			throw new NotImplementedException();
		}

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task Delete(List<TblWfwOrderProcessStepDto> dto)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TblWfwOrderProcessStepDto>> GetAll()
		{
			var TblWfwOrderProcessSteps = TblWfwOrderProcessStepRepository.GetAll();

			return TblWfwOrderProcessStepConverter.ToDtos(TblWfwOrderProcessSteps);

		}
		public Task<(List<TblWfwOrderProcessStepDto>, int)> GetAllByFilter(int? requestStatusId, int? orderStatusId, int? indexDocument, int? customerId, int? roleId, int? createdById, int? maxResult, int? page, int? rowInPage, string sort, string sidx, string factorNo = "", string? fromDate = null, string? toDate = null)
		{
			var TblWfwOrderProcessSteps = TblWfwOrderProcessStepRepository.GetAllByFilter(requestStatusId, orderStatusId, indexDocument, customerId, roleId, createdById, maxResult, page, rowInPage, sort, sidx, factorNo, fromDate, toDate);
			return Task.FromResult((TblWfwOrderProcessStepConverter.ToDtosWithRelated(TblWfwOrderProcessSteps.Result.Item1, 3), TblWfwOrderProcessSteps.Result.Item2));

		}

		public async Task<TblWfwOrderProcessStepDto> GetById(long id)
		{
			var TblWfwOrderProcessStep = TblWfwOrderProcessStepRepository.GetByKey(id);
			return TblWfwOrderProcessStepConverter.ToDtoWithRelated(TblWfwOrderProcessStep, 5);
		}

		public Task Update(TblWfwOrderProcessStepDto dto)
		{
			var item = dto.ToEntity();
			return TblWfwOrderProcessStepRepository.Update(item);
		}
		public Task UpdateStatus(TblWfwOrderProcessStepDto dto, int? relationStepId = null)
		{
			var itemorder = TblWfwOrderProcessStepRepository.GetByKey(dto.OrderProcessStepId);
			if (itemorder == null)
			{
				return Task.CompletedTask;
			}

			// Only in-progress steps can be closed and moved to next step.
			if (itemorder.StatusId != 1)
			{
				return Task.CompletedTask;
			}

			TblWfwOrderProcessStepRepository.Update(new TblWfwOrderProcessStep
			{
				OrderProcessStepId = dto.OrderProcessStepId,
				StatusId = dto.StatusId,
				DateComplete = dto.DateComplete,
				UserId = dto.UserId,
			});

			(List<TblWfwProcessStep>, int) itemProcess = tblWfwProcessStepRepository.GetAll(itemorder.TblWfwOrderProcess.ProcessId, null, null, null,null,null).Result;

			var processSteps = itemProcess.Item1 ?? new List<TblWfwProcessStep>();
			var currentProcessStep = processSteps.SingleOrDefault(x => x.ProcessStepId == itemorder.ProcessStepId);
			if (currentProcessStep == null)
			{
				return Task.CompletedTask;
			}

			var nextProcessStepId = ResolveNextProcessStepId(processSteps, currentProcessStep, relationStepId);
			if (nextProcessStepId.HasValue)
			{
				TblWfwOrderProcessStepRepository.Add(new TblWfwOrderProcessStep
				{
					OrderProcessId = itemorder.OrderProcessId,
					StatusId = 1,
					ProcessStepId = nextProcessStepId.Value,
					DateCreate = DateTime.Now,
				});

			}
			else
			{
				itemorder.TblWfwOrderProcess.StatusId = 3;
				itemorder.TblWfwOrderProcess.DateComplete = DateTime.Now;
				itemorder.TblWfwOrderProcess.TblLnsOrder.StatusId = 3;

				tblWfwOrderProcessRepository.Update(itemorder.TblWfwOrderProcess);
			}
			return Task.CompletedTask;
		}

		private int? ResolveNextProcessStepId(IList<TblWfwProcessStep> processSteps, TblWfwProcessStep currentProcessStep, int? forcedRelationStepId)
		{
			var activeProcessSteps = processSteps
				.Where(x => x.IsActive.HasValue && x.IsActive == 1)
				.OrderBy(x => x.OrderId)
				.ToList();

			var activeStepIds = activeProcessSteps.Select(x => x.ProcessStepId).ToHashSet();

			var relationsFromCurrent = (tblWfwRelationStepRepository.GetAll() ?? Array.Empty<TblWfwRelationStep>())
				.Where(x => x.FromProcessStepId == currentProcessStep.ProcessStepId && x.ToProcessStepId.HasValue)
				.ToList();

			if (forcedRelationStepId.HasValue && forcedRelationStepId.Value > 0)
			{
				var explicitTarget = relationsFromCurrent
					.FirstOrDefault(x => x.RelationStepId == forcedRelationStepId.Value)
					?.ToProcessStepId;

				if (explicitTarget.HasValue && activeStepIds.Contains(explicitTarget.Value))
				{
					return explicitTarget.Value;
				}
			}

			// If explicit relations are defined for this step, respect them first.
			var relationTargets = relationsFromCurrent
				.Select(x => x.ToProcessStepId!.Value)
				.Where(activeStepIds.Contains)
				.Distinct()
				.ToList();

			if (relationTargets.Count > 0)
			{
				return activeProcessSteps
					.Where(x => relationTargets.Contains(x.ProcessStepId))
					.OrderBy(x => x.OrderId)
					.Select(x => (int?)x.ProcessStepId)
					.FirstOrDefault();
			}

			// Backward-compatible fallback to sequential next step by OrderId.
			return activeProcessSteps
				.Where(x => x.OrderId > currentProcessStep.OrderId && x.ProcessStepId != currentProcessStep.ProcessStepId)
				.OrderBy(x => x.OrderId)
				.Select(x => (int?)x.ProcessStepId)
				.FirstOrDefault();
		}


		public Task Update(List<TblWfwOrderProcessStepDto> dto)
		{
			throw new NotImplementedException();
		}
	}
}
