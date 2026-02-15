

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RequestProcessStepConverter
    {

        public static RequestProcessStepDto ToDto(this RequestProcessStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RequestProcessStepDto ToDtoWithRelated(this RequestProcessStep source, int level)
        {
            if (source == null)
              return null;

            var target = new RequestProcessStepDto();

            // Properties
            target.Datecreate = source.Datecreate;
            target.RequestProcessId = source.RequestProcessId;
            target.RequestProcessStepId = source.RequestProcessStepId;
            target.RoleId = source.RoleId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // Navigation Properties
            if (level > 0) {
              target.RequestProcess = source.RequestProcess.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static RequestProcessStep ToEntity(this RequestProcessStepDto source)
        {
            if (source == null)
              return null;

            var target = new RequestProcessStep();

            // Properties
            target.Datecreate = source.Datecreate;
            target.RequestProcessId = source.RequestProcessId;
            target.RequestProcessStepId = source.RequestProcessStepId;
            target.RoleId = source.RoleId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RequestProcessStepDto> ToDtos(this IEnumerable<RequestProcessStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RequestProcessStepDto> ToDtosWithRelated(this IEnumerable<RequestProcessStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<RequestProcessStep> ToEntities(this IEnumerable<RequestProcessStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(RequestProcessStep source, RequestProcessStepDto target);

        static partial void OnEntityCreating(RequestProcessStepDto source, RequestProcessStep target);

    }

}
