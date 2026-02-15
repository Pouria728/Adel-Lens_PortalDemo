using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RoleToRoleConverter
    {

        public static RoleToRoleDto ToDto(this RoleToRole source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RoleToRoleDto ToDtoWithRelated(this RoleToRole source, int level)
        {
            if (source == null)
              return null;

            var target = new RoleToRoleDto();

            // Properties
            target.RoleId = source.RoleId;
            target.RolesId = source.RolesId;
            target.RoleToRolesId = source.RoleToRolesId;

            // Navigation Properties
            if (level > 0) {
              target.Role_RoleId = source.Role_RoleId.ToDtoWithRelated(level - 1);
              target.Role_RolesId = source.Role_RolesId.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static RoleToRole ToEntity(this RoleToRoleDto source)
        {
            if (source == null)
              return null;

            var target = new RoleToRole();

            // Properties
            target.RoleId = source.RoleId;
            target.RolesId = source.RolesId;
            target.RoleToRolesId = source.RoleToRolesId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RoleToRoleDto> ToDtos(this IEnumerable<RoleToRole> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RoleToRoleDto> ToDtosWithRelated(this IEnumerable<RoleToRole> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<RoleToRole> ToEntities(this IEnumerable<RoleToRoleDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(RoleToRole source, RoleToRoleDto target);

        static partial void OnEntityCreating(RoleToRoleDto source, RoleToRole target);

    }

}
