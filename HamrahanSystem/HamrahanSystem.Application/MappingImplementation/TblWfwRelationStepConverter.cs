using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwRelationStepConverter
    {

        public static TblWfwRelationStepDto ToDto(this TblWfwRelationStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwRelationStepDto ToDtoWithRelated(this TblWfwRelationStep source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwRelationStepDto();

            // Properties
            target.FromProcessStepId = source.FromProcessStepId;
            target.RelationStepId = source.RelationStepId;
            target.StepActionId = source.StepActionId;
            target.ToProcessStepId = source.ToProcessStepId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwRelationStep ToEntity(this TblWfwRelationStepDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwRelationStep();

            // Properties
            target.FromProcessStepId = source.FromProcessStepId;
            target.RelationStepId = source.RelationStepId;
            target.StepActionId = source.StepActionId;
            target.ToProcessStepId = source.ToProcessStepId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwRelationStepDto> ToDtos(this IEnumerable<TblWfwRelationStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwRelationStepDto> ToDtosWithRelated(this IEnumerable<TblWfwRelationStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwRelationStep> ToEntities(this IEnumerable<TblWfwRelationStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwRelationStep source, TblWfwRelationStepDto target);

        static partial void OnEntityCreating(TblWfwRelationStepDto source, TblWfwRelationStep target);

    }

}
