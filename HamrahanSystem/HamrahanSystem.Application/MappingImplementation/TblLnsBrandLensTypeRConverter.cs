using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsBrandLensTypeRConverter
    {

        public static TblLnsBrandLensTypeRDto ToDto(this TblLnsBrandLensTypeR source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsBrandLensTypeRDto ToDtoWithRelated(this TblLnsBrandLensTypeR source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandLensTypeRDto();

            // Properties
            target.BrandId = source.BrandId;
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.LensTypeRId = source.LensTypeRId;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrand = source.TblLnsBrand.ToDtoWithRelated(level - 1);
              target.TblLnsLensTypeR = source.TblLnsLensTypeR.ToDtoWithRelated(level - 1);
              target.TblLnsLensTypeRLensIndexRs = source.TblLnsLensTypeRLensIndexRs.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsBrandLensTypeR ToEntity(this TblLnsBrandLensTypeRDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandLensTypeR();

            // Properties
            target.BrandId = source.BrandId;
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.LensTypeRId = source.LensTypeRId;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsBrandLensTypeRDto> ToDtos(this IEnumerable<TblLnsBrandLensTypeR> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsBrandLensTypeRDto> ToDtosWithRelated(this IEnumerable<TblLnsBrandLensTypeR> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsBrandLensTypeR> ToEntities(this IEnumerable<TblLnsBrandLensTypeRDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsBrandLensTypeR source, TblLnsBrandLensTypeRDto target);

        static partial void OnEntityCreating(TblLnsBrandLensTypeRDto source, TblLnsBrandLensTypeR target);

    }

}
