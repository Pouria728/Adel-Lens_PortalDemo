using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Repository;

namespace HamrahanSystem.Application.UseCaseImplementation
{
	public class UserActivityLogService(IUserActivityLogRepository logRepository, IUserActivityLogDetailRepository detailRepository) : IUserActivityLogService
	{
		public Task Add(UserActivityLogDto dto)
		{
			var item = dto.ToEntity();
			return logRepository.Add(item);
		}

		public Task AddDetails(List<UserActivityLogDetailDto> items)
		{
			var entities = UserActivityLogDetailConverter.ToEntities(items);
			return detailRepository.AddRange(entities);
		}

		public Task<(List<UserActivityLogDto>, int)> GetAll(int? maxResult, int? page, int? rowInPage, string sort, string sidx, DateTime? fromDate, DateTime? toDate, string? userName)
		{
			var logs = logRepository.GetAll(maxResult, page, rowInPage, sort, sidx, fromDate, toDate, userName);
			return Task.FromResult((UserActivityLogConverter.ToDtos(logs.Result.Item1), logs.Result.Item2));
		}

		public async Task<UserActivityLogDto> GetByCorrelationId(Guid correlationId)
		{
			var log = await logRepository.GetByCorrelationId(correlationId);
			return log.ToDto();
		}

		public async Task<List<UserActivityLogDetailDto>> GetDetails(Guid correlationId)
		{
			var details = await detailRepository.GetByCorrelationId(correlationId);
			return UserActivityLogDetailConverter.ToDtos(details);
		}
	}
}
