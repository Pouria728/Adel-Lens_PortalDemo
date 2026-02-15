

using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RequestProcessConverter
    {

        public static RequestProcessDto ToDto(this RequestProcess source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RequestProcessDto ToDtoWithRelated(this RequestProcess source, int level)
        {
            if (source == null)
              return null;

            var target = new RequestProcessDto();

            // Properties
            target.CreateDate = source.CreateDate;
            target.DateComplete = source.DateComplete;
            target.RequestId = source.RequestId;
            target.RequestProcessId = source.RequestProcessId;
            target.StatusId = source.StatusId;

            // Navigation Properties
            if (level > 0) {
              target.Request = source.Request.ToDtoWithRelated(level - 1);
              target.RequestProcessSteps = source.RequestProcessSteps.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static RequestProcess ToEntity(this RequestProcessDto source)
        {
            if (source == null)
              return null;

            var target = new RequestProcess();

            // Properties
            target.CreateDate = source.CreateDate;
            target.DateComplete = source.DateComplete;
            target.RequestId = source.RequestId;
            target.RequestProcessId = source.RequestProcessId;
            target.StatusId = source.StatusId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RequestProcessDto> ToDtos(this IEnumerable<RequestProcess> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RequestProcessDto> ToDtosWithRelated(this IEnumerable<RequestProcess> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<RequestProcess> ToEntities(this IEnumerable<RequestProcessDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(RequestProcess source, RequestProcessDto target);

        static partial void OnEntityCreating(RequestProcessDto source, RequestProcess target);

    }

}
