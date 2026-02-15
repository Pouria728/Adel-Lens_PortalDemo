

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class PermissionConverter
    {

        public static PermissionDto ToDto(this Permission source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static PermissionDto ToDtoWithRelated(this Permission source, int level)
        {
            if (source == null)
              return null;

            var target = new PermissionDto();

            // Properties
            target.PermissionId = source.PermissionId;
            target.PermissionKey = source.PermissionKey;
            target.Title = source.Title;

            // Navigation Properties
            if (level > 0) {
              target.RolePermissions = source.RolePermissions.ToDtosWithRelated(level - 1);
              target.UserPermissions = source.UserPermissions.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static Permission ToEntity(this PermissionDto source)
        {
            if (source == null)
              return null;

            var target = new Permission();

            // Properties
            target.PermissionId = source.PermissionId;
            target.PermissionKey = source.PermissionKey;
            target.Title = source.Title;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<PermissionDto> ToDtos(this IEnumerable<Permission> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<PermissionDto> ToDtos(this List<Permission> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<PermissionDto> ToDtosWithRelated(this IEnumerable<Permission> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<PermissionDto> ToDtosWithRelated(this List<Permission> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<Permission> ToEntities(this IEnumerable<PermissionDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<Permission> ToEntities(this List<PermissionDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(Permission source, PermissionDto target);

        static partial void OnEntityCreating(PermissionDto source, Permission target);

    }

}
