using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
	public static partial class UserActivityLogConverter
	{
		public static UserActivityLogDto ToDto(this UserActivityLog source)
		{
			if (source == null)
				return null;

			var target = new UserActivityLogDto
			{
				ActivityLogId = source.ActivityLogId,
				CorrelationId = source.CorrelationId,
				UserId = source.UserId,
				UserName = source.UserName,
				ActionType = source.ActionType,
				Controller = source.Controller,
				Action = source.Action,
				FormName = source.FormName,
				HttpMethod = source.HttpMethod,
				Path = source.Path,
				QueryString = source.QueryString,
				RequestBody = source.RequestBody,
				StatusCode = source.StatusCode,
				DurationMs = source.DurationMs,
				IpAddress = source.IpAddress,
				UserAgent = source.UserAgent,
				CreatedAt = source.CreatedAt
			};

			if (source.Details != null && source.Details.Count > 0)
			{
				target.Details = source.Details.ToDtos();
			}

			return target;
		}

		public static UserActivityLog ToEntity(this UserActivityLogDto source)
		{
			if (source == null)
				return null;

			var target = new UserActivityLog
			{
				ActivityLogId = source.ActivityLogId,
				CorrelationId = source.CorrelationId,
				UserId = source.UserId,
				UserName = source.UserName,
				ActionType = source.ActionType,
				Controller = source.Controller,
				Action = source.Action,
				FormName = source.FormName,
				HttpMethod = source.HttpMethod,
				Path = source.Path,
				QueryString = source.QueryString,
				RequestBody = source.RequestBody,
				StatusCode = source.StatusCode,
				DurationMs = source.DurationMs,
				IpAddress = source.IpAddress,
				UserAgent = source.UserAgent,
				CreatedAt = source.CreatedAt
			};

			if (source.Details != null && source.Details.Count > 0)
			{
				target.Details = source.Details.ToEntities();
			}

			return target;
		}

		public static List<UserActivityLogDto> ToDtos(this IEnumerable<UserActivityLog> source)
		{
			if (source == null)
				return null;

			return source.Select(x => x.ToDto()).ToList();
		}

		public static List<UserActivityLog> ToEntities(this IEnumerable<UserActivityLogDto> source)
		{
			if (source == null)
				return null;

			return source.Select(x => x.ToEntity()).ToList();
		}
	}
}
