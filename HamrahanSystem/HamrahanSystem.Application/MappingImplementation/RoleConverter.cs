

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RoleConverter
    {

        public static RoleDto ToDto(this Role source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RoleDto ToDtoWithRelated(this Role source, int level)
        {
            if (source == null)
              return null;

            var target = new RoleDto();

            // Properties
            target.RoleId = source.RoleId;
            target.RoleKey = source.RoleKey;
            target.RoleName = source.RoleName;
            target.TenantId = source.TenantId;

            // Navigation Properties
            if (level > 0) {
              target.RolePermissions = source.RolePermissions.ToDtosWithRelated(level - 1);
              target.RoleToRoles_RoleId = source.RoleToRoles_RoleId.ToDtosWithRelated(level - 1);
              target.RoleToRoles_RolesId = source.RoleToRoles_RolesId.ToDtosWithRelated(level - 1);
              target.UserRoles = source.UserRoles.ToDtosWithRelated(level - 1);
				target.TblWfwRoleStepes = source.TblWfwRoleStepes.ToDtosWithRelated(level - 1);
			}

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static Role ToEntity(this RoleDto source)
        {
            if (source == null)
              return null;

            var target = new Role();

            // Properties
            target.RoleId = source.RoleId;
            target.RoleKey = source.RoleKey;
            target.RoleName = source.RoleName;
            target.TenantId = source.TenantId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RoleDto> ToDtos(this IEnumerable<Role> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RoleDto> ToDtosWithRelated(this IEnumerable<Role> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<Role> ToEntities(this IEnumerable<RoleDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(Role source, RoleDto target);

        static partial void OnEntityCreating(RoleDto source, Role target);

    }

}
