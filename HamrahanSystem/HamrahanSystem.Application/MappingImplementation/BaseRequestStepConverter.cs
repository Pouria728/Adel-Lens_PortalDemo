

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class BaseRequestStepConverter
    {

        public static BaseRequestStepDto ToDto(this BaseRequestStep source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static BaseRequestStepDto ToDtoWithRelated(this BaseRequestStep source, int level)
        {
            if (source == null)
              return null;

            var target = new BaseRequestStepDto();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.BaseRequestStepId = source.BaseRequestStepId;
            target.OrderId = source.OrderId;
            target.RoleId = source.RoleId;
            target.Title = source.Title;

            // Navigation Properties
            if (level > 0) {
              target.BaseRequeste = source.BaseRequeste.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static BaseRequestStep ToEntity(this BaseRequestStepDto source)
        {
            if (source == null)
              return null;

            var target = new BaseRequestStep();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.BaseRequestStepId = source.BaseRequestStepId;
            target.OrderId = source.OrderId;
            target.RoleId = source.RoleId;
            target.Title = source.Title;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<BaseRequestStepDto> ToDtos(this IEnumerable<BaseRequestStep> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<BaseRequestStepDto> ToDtosWithRelated(this IEnumerable<BaseRequestStep> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<BaseRequestStep> ToEntities(this IEnumerable<BaseRequestStepDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(BaseRequestStep source, BaseRequestStepDto target);

        static partial void OnEntityCreating(BaseRequestStepDto source, BaseRequestStep target);

    }

}
