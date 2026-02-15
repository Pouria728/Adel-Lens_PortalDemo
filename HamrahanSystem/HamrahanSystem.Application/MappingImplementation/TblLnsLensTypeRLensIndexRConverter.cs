using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensTypeRLensIndexRConverter
    {

        public static TblLnsLensTypeRLensIndexRDto ToDto(this TblLnsLensTypeRLensIndexR source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensTypeRLensIndexRDto ToDtoWithRelated(this TblLnsLensTypeRLensIndexR source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensTypeRLensIndexRDto();

            // Properties
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.LensIndexRId = source.LensIndexRId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsLensIndexRSphs = source.TblLnsLensIndexRSphs.ToDtosWithRelated(level - 1);
              target.TblLnsLensIndexR = source.TblLnsLensIndexR.ToDtoWithRelated(level - 1);
              target.TblLnsBrandLensTypeR = source.TblLnsBrandLensTypeR.ToDtoWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensTypeRLensIndexR ToEntity(this TblLnsLensTypeRLensIndexRDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensTypeRLensIndexR();

            // Properties
            target.BrandLensTypeRId = source.BrandLensTypeRId;
            target.LensIndexRId = source.LensIndexRId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensTypeRLensIndexRDto> ToDtos(this IEnumerable<TblLnsLensTypeRLensIndexR> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsLensTypeRLensIndexRDto> ToDtosWithRelated(this IEnumerable<TblLnsLensTypeRLensIndexR> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsLensTypeRLensIndexR> ToEntities(this IEnumerable<TblLnsLensTypeRLensIndexRDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensTypeRLensIndexR source, TblLnsLensTypeRLensIndexRDto target);

        static partial void OnEntityCreating(TblLnsLensTypeRLensIndexRDto source, TblLnsLensTypeRLensIndexR target);

    }

}
