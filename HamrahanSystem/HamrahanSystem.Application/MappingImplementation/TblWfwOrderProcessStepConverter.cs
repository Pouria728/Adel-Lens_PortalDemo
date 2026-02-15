using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblWfwOrderProcessStepConverter
    {

        public static TblWfwOrderProcessStepDto ToDto(this TblWfwOrderProcessStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblWfwOrderProcessStepDto ToDtoWithRelated(this TblWfwOrderProcessStep source, int level)
        {
            if (source == null)
              return null;

            var target = new TblWfwOrderProcessStepDto();

            // Properties
            target.DateComplete = source.DateComplete;
            target.DateCreate = source.DateCreate;
            target.OrderProcessId = source.OrderProcessId;
            target.OrderProcessStepId = source.OrderProcessStepId;
            target.ProcessStepId = source.ProcessStepId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // Navigation Properties
            if (level > 0) {
              target.TblWfwOrderProcess = source.TblWfwOrderProcess.ToDtoWithRelated(level - 1);
              target.User = source.User.ToDtoWithRelated(level - 1);
              target.TblWfwProcessStep = source.TblWfwProcessStep.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblWfwOrderProcessStep ToEntity(this TblWfwOrderProcessStepDto source)
        {
            if (source == null)
              return null;

            var target = new TblWfwOrderProcessStep();

            // Properties
            target.DateComplete = source.DateComplete;
            target.DateCreate = source.DateCreate;
            target.OrderProcessId = source.OrderProcessId;
            target.OrderProcessStepId = source.OrderProcessStepId;
            target.ProcessStepId = source.ProcessStepId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblWfwOrderProcessStepDto> ToDtos(this IEnumerable<TblWfwOrderProcessStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblWfwOrderProcessStepDto> ToDtosWithRelated(this IEnumerable<TblWfwOrderProcessStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblWfwOrderProcessStep> ToEntities(this IEnumerable<TblWfwOrderProcessStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblWfwOrderProcessStep source, TblWfwOrderProcessStepDto target);

        static partial void OnEntityCreating(TblWfwOrderProcessStepDto source, TblWfwOrderProcessStep target);

    }

}
