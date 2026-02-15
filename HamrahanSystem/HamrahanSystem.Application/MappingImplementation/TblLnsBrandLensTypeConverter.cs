using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsBrandLensTypeConverter
    {

        public static TblLnsBrandLensTypeDto ToDto(this TblLnsBrandLensType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsBrandLensTypeDto ToDtoWithRelated(this TblLnsBrandLensType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandLensTypeDto();

            // Properties
            target.BrandId = source.BrandId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.LensTypeId = source.LensTypeId;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrandDesignTypes = source.TblLnsBrandDesignTypes.ToDtosWithRelated(level - 1);
              target.TblLnsLensType = source.TblLnsLensType.ToDtoWithRelated(level - 1);
              target.TblLnsBrand = source.TblLnsBrand.ToDtoWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsBrandLensType ToEntity(this TblLnsBrandLensTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandLensType();

            // Properties
            target.BrandId = source.BrandId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.LensTypeId = source.LensTypeId;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsBrandLensTypeDto> ToDtos(this IEnumerable<TblLnsBrandLensType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsBrandLensTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsBrandLensType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsBrandLensType> ToEntities(this IEnumerable<TblLnsBrandLensTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsBrandLensType source, TblLnsBrandLensTypeDto target);

        static partial void OnEntityCreating(TblLnsBrandLensTypeDto source, TblLnsBrandLensType target);

    }

}
