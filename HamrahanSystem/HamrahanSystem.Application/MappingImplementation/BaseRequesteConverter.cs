

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class BaseRequesteConverter
    {

        public static BaseRequesteDto ToDto(this BaseRequeste source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static BaseRequesteDto ToDtoWithRelated(this BaseRequeste source, int level)
        {
            if (source == null)
                return null;

            var target = new BaseRequesteDto();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.DateCreate = source.DateCreate;
            target.IsActive = source.IsActive;
            target.Title = source.Title;
            target.TypeRequest = source.TypeRequest;

            // Navigation Properties
            if (level > 0)
            {
                target.BaseRequestSteps = source.BaseRequestSteps.ToDtosWithRelated(level - 1);
                target.Requests = source.Requests.ToDtosWithRelated(level - 1);
                target.BaseRequestFieldes = source.BaseRequestFieldes.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static BaseRequeste ToEntity(this BaseRequesteDto source)
        {
            if (source == null)
                return null;

            var target = new BaseRequeste();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.DateCreate = source.DateCreate;
            target.IsActive = source.IsActive;
            target.Title = source.Title;
            target.TypeRequest = source.TypeRequest;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<BaseRequesteDto> ToDtos(this IEnumerable<BaseRequeste> source)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<BaseRequesteDto> ToDtosWithRelated(this IEnumerable<BaseRequeste> source, int level)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<BaseRequeste> ToEntities(this IEnumerable<BaseRequesteDto> source)
        {
            if (source == null)
                return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(BaseRequeste source, BaseRequesteDto target);

        static partial void OnEntityCreating(BaseRequesteDto source, BaseRequeste target);

    }

}



