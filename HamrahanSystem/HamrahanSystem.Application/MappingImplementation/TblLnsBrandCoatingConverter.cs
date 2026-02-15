using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsBrandCoatingConverter
    {

        public static TblLnsBrandCoatingDto ToDto(this TblLnsBrandCoating source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsBrandCoatingDto ToDtoWithRelated(this TblLnsBrandCoating source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandCoatingDto();

            // Properties
            target.BrandCoatingId = source.BrandCoatingId;
            target.BrandId = source.BrandId;
            target.CoatingId = source.CoatingId;
            target.IsDefault = source.IsDefault;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrand = source.TblLnsBrand.ToDtoWithRelated(level - 1);
              target.TblLnsCoating = source.TblLnsCoating.ToDtoWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsBrandCoating ToEntity(this TblLnsBrandCoatingDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandCoating();

            // Properties
            target.BrandCoatingId = source.BrandCoatingId;
            target.BrandId = source.BrandId;
            target.CoatingId = source.CoatingId;
            target.IsDefault = source.IsDefault;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsBrandCoatingDto> ToDtos(this IEnumerable<TblLnsBrandCoating> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsBrandCoatingDto> ToDtosWithRelated(this IEnumerable<TblLnsBrandCoating> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsBrandCoating> ToEntities(this IEnumerable<TblLnsBrandCoatingDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsBrandCoating source, TblLnsBrandCoatingDto target);

        static partial void OnEntityCreating(TblLnsBrandCoatingDto source, TblLnsBrandCoating target);

    }

}
