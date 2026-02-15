using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
	public static partial class UserActivityLogDetailConverter
	{
		public static UserActivityLogDetailDto ToDto(this UserActivityLogDetail source)
		{
			if (source == null)
				return null;

			return new UserActivityLogDetailDto
			{
				ActivityLogDetailId = source.ActivityLogDetailId,
				CorrelationId = source.CorrelationId,
				UserId = source.UserId,
				UserName = source.UserName,
				EntityName = source.EntityName,
				EntityId = source.EntityId,
				Operation = source.Operation,
				Changes = source.Changes,
				CreatedAt = source.CreatedAt
			};
		}

		public static UserActivityLogDetail ToEntity(this UserActivityLogDetailDto source)
		{
			if (source == null)
				return null;

			return new UserActivityLogDetail
			{
				ActivityLogDetailId = source.ActivityLogDetailId,
				CorrelationId = source.CorrelationId,
				UserId = source.UserId,
				UserName = source.UserName,
				EntityName = source.EntityName,
				EntityId = source.EntityId,
				Operation = source.Operation,
				Changes = source.Changes,
				CreatedAt = source.CreatedAt
			};
		}

		public static List<UserActivityLogDetailDto> ToDtos(this IEnumerable<UserActivityLogDetail> source)
		{
			if (source == null)
				return null;

			return source.Select(x => x.ToDto()).ToList();
		}

		public static List<UserActivityLogDetail> ToEntities(this IEnumerable<UserActivityLogDetailDto> source)
		{
			if (source == null)
				return null;

			return source.Select(x => x.ToEntity()).ToList();
		}
	}
}
