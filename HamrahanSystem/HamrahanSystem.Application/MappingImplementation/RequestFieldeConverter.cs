

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RequestFieldeConverter
    {

        public static RequestFieldeDto ToDto(this RequestFielde source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RequestFieldeDto ToDtoWithRelated(this RequestFielde source, int level)
        {
            if (source == null)
              return null;

            var target = new RequestFieldeDto();

            // Properties
            target.BaseRequestFieldId = source.BaseRequestFieldId;
            target.DataValue = source.DataValue;
            target.RequestFieldId = source.RequestFieldId;
            target.RequestId = source.RequestId;

            // Navigation Properties
            if (level > 0) {
              target.Request = source.Request.ToDtoWithRelated(level - 1);
              target.BaseRequestFielded = source.BaseRequestFielded.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static RequestFielde ToEntity(this RequestFieldeDto source)
        {
            if (source == null)
              return null;

            var target = new RequestFielde();

            // Properties
            target.BaseRequestFieldId = source.BaseRequestFieldId;
            target.DataValue = source.DataValue;
            target.RequestFieldId = source.RequestFieldId;
            target.RequestId = source.RequestId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RequestFieldeDto> ToDtos(this IEnumerable<RequestFielde> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RequestFieldeDto> ToDtosWithRelated(this IEnumerable<RequestFielde> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<RequestFielde> ToEntities(this IEnumerable<RequestFieldeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(RequestFielde source, RequestFieldeDto target);

        static partial void OnEntityCreating(RequestFieldeDto source, RequestFielde target);

    }

}
