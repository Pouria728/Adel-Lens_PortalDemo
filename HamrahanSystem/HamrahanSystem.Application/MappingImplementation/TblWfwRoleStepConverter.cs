using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwRoleStepConverter
    {

        public static TblWfwRoleStepDto ToDto(this TblWfwRoleStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwRoleStepDto ToDtoWithRelated(this TblWfwRoleStep source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwRoleStepDto();

            // Properties
            target.ProcessStepId = source.ProcessStepId;
            target.RoleId = source.RoleId;
            target.RoleStepId = source.RoleStepId;
			if (level > 0)
			{
				target.TblWfwProcessStep = source.TblWfwProcessStep.ToDtoWithRelated(level - 1);
				target.Role = source.Role.ToDtoWithRelated(level - 1);
				
			}
			// User-defined partial method
			OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwRoleStep ToEntity(this TblWfwRoleStepDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwRoleStep();

            // Properties
            target.ProcessStepId = source.ProcessStepId;
            target.RoleId = source.RoleId;
            target.RoleStepId = source.RoleStepId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwRoleStepDto> ToDtos(this IEnumerable<TblWfwRoleStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwRoleStepDto> ToDtosWithRelated(this IEnumerable<TblWfwRoleStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwRoleStep> ToEntities(this IEnumerable<TblWfwRoleStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwRoleStep source, TblWfwRoleStepDto target);

        static partial void OnEntityCreating(TblWfwRoleStepDto source, TblWfwRoleStep target);

    }

}
