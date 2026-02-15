using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

	public static partial class UserPermissionConverter
	{

		public static UserPermissionDto ToDto(this UserPermission source)
		{
			return source.ToDtoWithRelated(0);
		}

		public static UserPermissionDto ToDtoWithRelated(this UserPermission source, int level)
		{
			if (source == null)
				return null;

			var target = new UserPermissionDto();

			// Properties
			target.Granted = source.Granted;
			target.PermissionKey = source.PermissionKey;
			target.UserId = source.UserId;
			target.UserPermissionId = source.UserPermissionId;

			// Navigation Properties
			if (level > 0)
			{
				target.User = source.User.ToDtoWithRelated(level - 1);
				target.Permission = source.Permission.ToDtoWithRelated(level - 1);
			}

			// User-defined partial method
			OnDtoCreating(source, target);

			return target;
		}

		public static UserPermission ToEntity(this UserPermissionDto source)
		{
			if (source == null)
				return null;

			var target = new UserPermission();

			// Properties
			target.Granted = source.Granted;
			target.PermissionKey = source.PermissionKey;
			target.UserId = source.UserId;
			target.UserPermissionId = source.UserPermissionId;

			// User-defined partial method
			OnEntityCreating(source, target);

			return target;
		}

		public static List<UserPermissionDto> ToDtos(this IEnumerable<UserPermission> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}
		public static List<UserPermissionDto> ToDtos(this List<UserPermission> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<UserPermissionDto> ToDtosWithRelated(this IEnumerable<UserPermission> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}
		public static List<UserPermissionDto> ToDtosWithRelated(this List<UserPermission> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<UserPermission> ToEntities(this IEnumerable<UserPermissionDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}
		public static List<UserPermission> ToEntities(this List<UserPermissionDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(UserPermission source, UserPermissionDto target);

		static partial void OnEntityCreating(UserPermissionDto source, UserPermission target);

	}

}
