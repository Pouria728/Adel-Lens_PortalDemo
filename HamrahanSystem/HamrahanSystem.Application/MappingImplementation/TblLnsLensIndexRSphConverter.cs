using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensIndexRSphConverter
    {

        public static TblLnsLensIndexRSphDto ToDto(this TblLnsLensIndexRSph source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensIndexRSphDto ToDtoWithRelated(this TblLnsLensIndexRSph source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexRSphDto();

            // Properties
            target.LensIndexRSphId = source.LensIndexRSphId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.OrderId = source.OrderId;
            target.SphId = source.SphId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsSph = source.TblLnsSph.ToDtoWithRelated(level - 1);
              target.TblLnsLensTypeRLensIndexR = source.TblLnsLensTypeRLensIndexR.ToDtoWithRelated(level - 1);
              target.TblLnsOrderItems = source.TblLnsOrderItems.ToDtosWithRelated(level - 1);
              target.TblLnsSphCyls = source.TblLnsSphCyls.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensIndexRSph ToEntity(this TblLnsLensIndexRSphDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexRSph();

            // Properties
            target.LensIndexRSphId = source.LensIndexRSphId;
            target.LensTypeRLensIndexRId = source.LensTypeRLensIndexRId;
            target.OrderId = source.OrderId;
            target.SphId = source.SphId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensIndexRSphDto> ToDtos(this IEnumerable<TblLnsLensIndexRSph> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsLensIndexRSphDto> ToDtosWithRelated(this IEnumerable<TblLnsLensIndexRSph> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsLensIndexRSph> ToEntities(this IEnumerable<TblLnsLensIndexRSphDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensIndexRSph source, TblLnsLensIndexRSphDto target);

        static partial void OnEntityCreating(TblLnsLensIndexRSphDto source, TblLnsLensIndexRSph target);

    }

}
