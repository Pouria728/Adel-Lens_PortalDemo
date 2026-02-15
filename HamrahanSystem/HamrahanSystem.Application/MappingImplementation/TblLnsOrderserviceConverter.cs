using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsOrderserviceConverter
    {

        public static TblLnsOrderserviceDto ToDto(this TblLnsOrderservice source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsOrderserviceDto ToDtoWithRelated(this TblLnsOrderservice source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsOrderserviceDto();

            // Properties
            target.DefineServiceId = source.DefineServiceId;
            target.OrderId = source.OrderId;
            target.OrderServicesId = source.OrderServicesId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsOrder = source.TblLnsOrder.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsOrderservice ToEntity(this TblLnsOrderserviceDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsOrderservice();

            // Properties
            target.DefineServiceId = source.DefineServiceId;
            target.OrderId = source.OrderId;
            target.OrderServicesId = source.OrderServicesId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsOrderserviceDto> ToDtos(this IEnumerable<TblLnsOrderservice> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsOrderserviceDto> ToDtosWithRelated(this IEnumerable<TblLnsOrderservice> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsOrderservice> ToEntities(this IEnumerable<TblLnsOrderserviceDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsOrderservice source, TblLnsOrderserviceDto target);

        static partial void OnEntityCreating(TblLnsOrderserviceDto source, TblLnsOrderservice target);

    }

}
