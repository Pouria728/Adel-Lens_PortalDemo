using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwRelationStepConditionConverter
    {

        public static TblWfwRelationStepConditionDto ToDto(this TblWfwRelationStepCondition source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwRelationStepConditionDto ToDtoWithRelated(this TblWfwRelationStepCondition source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwRelationStepConditionDto();

            // Properties
            target.ConditionId = source.ConditionId;
            target.OkResult = source.OkResult;
            target.RelationStepConditionId = source.RelationStepConditionId;
            target.RelationStepId = source.RelationStepId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwRelationStepCondition ToEntity(this TblWfwRelationStepConditionDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwRelationStepCondition();

            // Properties
            target.ConditionId = source.ConditionId;
            target.OkResult = source.OkResult;
            target.RelationStepConditionId = source.RelationStepConditionId;
            target.RelationStepId = source.RelationStepId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwRelationStepConditionDto> ToDtos(this IEnumerable<TblWfwRelationStepCondition> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwRelationStepConditionDto> ToDtosWithRelated(this IEnumerable<TblWfwRelationStepCondition> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwRelationStepCondition> ToEntities(this IEnumerable<TblWfwRelationStepConditionDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwRelationStepCondition source, TblWfwRelationStepConditionDto target);

        static partial void OnEntityCreating(TblWfwRelationStepConditionDto source, TblWfwRelationStepCondition target);

    }

}
