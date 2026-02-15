using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwStepActionConverter
    {

        public static TblWfwStepActionDto ToDto(this TblWfwStepAction source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwStepActionDto ToDtoWithRelated(this TblWfwStepAction source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwStepActionDto();

            // Properties
            target.ProcessActionId = source.ProcessActionId;
            target.ProcessStepId = source.ProcessStepId;
            target.StepActionId = source.StepActionId;

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwStepAction ToEntity(this TblWfwStepActionDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwStepAction();

            // Properties
            target.ProcessActionId = source.ProcessActionId;
            target.ProcessStepId = source.ProcessStepId;
            target.StepActionId = source.StepActionId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwStepActionDto> ToDtos(this IEnumerable<TblWfwStepAction> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwStepActionDto> ToDtosWithRelated(this IEnumerable<TblWfwStepAction> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwStepAction> ToEntities(this IEnumerable<TblWfwStepActionDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwStepAction source, TblWfwStepActionDto target);

        static partial void OnEntityCreating(TblWfwStepActionDto source, TblWfwStepAction target);

    }

}
