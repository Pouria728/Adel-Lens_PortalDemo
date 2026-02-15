

using System.Collections.Generic;
using System.Linq;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class RequestConverter
    {

        public static RequestDto ToDto(this Request source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static RequestDto ToDtoWithRelated(this Request source, int level)
        {
            if (source == null)
              return null;

            var target = new RequestDto();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.DateCreate = source.DateCreate;
            target.RequestId = source.RequestId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // Navigation Properties
            if (level > 0) {
              target.RequestFieldes = source.RequestFieldes.ToDtosWithRelated(level - 1);
              target.RequestProcesses = source.RequestProcesses.ToDtosWithRelated(level - 1);
              target.BaseRequeste = source.BaseRequeste.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static Request ToEntity(this RequestDto source)
        {
            if (source == null)
              return null;

            var target = new Request();

            // Properties
            target.BaseRequestId = source.BaseRequestId;
            target.DateCreate = source.DateCreate;
            target.RequestId = source.RequestId;
            target.StatusId = source.StatusId;
            target.UserId = source.UserId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<RequestDto> ToDtos(this IEnumerable<Request> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<RequestDto> ToDtosWithRelated(this IEnumerable<Request> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<Request> ToEntities(this IEnumerable<RequestDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(Request source, RequestDto target);

        static partial void OnEntityCreating(RequestDto source, Request target);

    }

}
