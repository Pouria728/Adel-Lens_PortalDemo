using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class UserRoleConverter
    {

        public static UserRoleDto ToDto(this UserRole source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static UserRoleDto ToDtoWithRelated(this UserRole source, int level)
        {
            if (source == null)
              return null;

            var target = new UserRoleDto();

            // Properties
            target.RoleId = source.RoleId;
            target.UserId = source.UserId;
            target.UserRoleId = source.UserRoleId;

            // Navigation Properties
            if (level > 0) {
              target.User = source.User.ToDtoWithRelated(level - 1);
              target.Role = source.Role.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static UserRole ToEntity(this UserRoleDto source)
        {
            if (source == null)
              return null;

            var target = new UserRole();

            // Properties
            target.RoleId = source.RoleId;
            target.UserId = source.UserId;
            target.UserRoleId = source.UserRoleId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<UserRoleDto> ToDtos(this IEnumerable<UserRole> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<UserRoleDto> ToDtosWithRelated(this IEnumerable<UserRole> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<UserRole> ToEntities(this IEnumerable<UserRoleDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(UserRole source, UserRoleDto target);

        static partial void OnEntityCreating(UserRoleDto source, UserRole target);

    }

}
