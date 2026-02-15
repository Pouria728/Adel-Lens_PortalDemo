using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsBrandDesignTypeConverter
    {

        public static TblLnsBrandDesignTypeDto ToDto(this TblLnsBrandDesignType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsBrandDesignTypeDto ToDtoWithRelated(this TblLnsBrandDesignType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandDesignTypeDto();

            // Properties
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.DesignTypeId = source.DesignTypeId;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsDesignType = source.TblLnsDesignType.ToDtoWithRelated(level - 1);
              target.TblLnsBrandLensType = source.TblLnsBrandLensType.ToDtoWithRelated(level - 1);
              target.TblLnsDesignTypeLensIndices = source.TblLnsDesignTypeLensIndices.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsBrandDesignType ToEntity(this TblLnsBrandDesignTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsBrandDesignType();

            // Properties
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.BrandLensTypeId = source.BrandLensTypeId;
            target.DesignTypeId = source.DesignTypeId;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsBrandDesignTypeDto> ToDtos(this IEnumerable<TblLnsBrandDesignType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsBrandDesignTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsBrandDesignType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsBrandDesignType> ToEntities(this IEnumerable<TblLnsBrandDesignTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsBrandDesignType source, TblLnsBrandDesignTypeDto target);

        static partial void OnEntityCreating(TblLnsBrandDesignTypeDto source, TblLnsBrandDesignType target);

    }

}
