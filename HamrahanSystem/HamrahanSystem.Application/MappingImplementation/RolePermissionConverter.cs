using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

	public static partial class RolePermissionConverter
	{

		public static RolePermissionDto ToDto(this RolePermission source)
		{
			return source.ToDtoWithRelated(0);
		}

		public static RolePermissionDto ToDtoWithRelated(this RolePermission source, int level)
		{
			if (source == null)
				return null;

			var target = new RolePermissionDto();

			// Properties
			target.PermissionKey = source.PermissionKey;
			target.RoleId = source.RoleId;
			target.RolePermissionId = source.RolePermissionId;
			target.PermissionId = source.PermissionId;

			// Navigation Properties
			if (level > 0)
			{
				target.Role = source.Role.ToDtoWithRelated(level - 1);
				target.Permission = source.Permission.ToDtoWithRelated(level - 1);
			}

			// User-defined partial method
			OnDtoCreating(source, target);

			return target;
		}

		public static RolePermission ToEntity(this RolePermissionDto source)
		{
			if (source == null)
				return null;

			var target = new RolePermission();

			// Properties
			target.PermissionKey = source.PermissionKey;
			target.PermissionId = source.PermissionId;
			target.RoleId = source.RoleId;
			target.RolePermissionId = source.RolePermissionId;
			

			// User-defined partial method
			OnEntityCreating(source, target);

			return target;
		}

		public static List<RolePermissionDto> ToDtos(this IEnumerable<RolePermission> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<RolePermissionDto> ToDtos(this List<RolePermission> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<RolePermissionDto> ToDtosWithRelated(this IEnumerable<RolePermission> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}
		public static List<RolePermissionDto> ToDtosWithRelated(this List<RolePermission> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<RolePermission> ToEntities(this IEnumerable<RolePermissionDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}
		public static List<RolePermission> ToEntities(this List<RolePermissionDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(RolePermission source, RolePermissionDto target);

		static partial void OnEntityCreating(RolePermissionDto source, RolePermission target);

	}

}
