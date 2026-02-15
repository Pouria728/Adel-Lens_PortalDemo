using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsDesignTypeLensIndexConverter
    {

        public static TblLnsDesignTypeLensIndexDto ToDto(this TblLnsDesignTypeLensIndex source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsDesignTypeLensIndexDto ToDtoWithRelated(this TblLnsDesignTypeLensIndex source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsDesignTypeLensIndexDto();

            // Properties
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.LensIndexId = source.LensIndexId;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsLensIndex = source.TblLnsLensIndex.ToDtoWithRelated(level - 1);
              target.TblLnsBrandDesignType = source.TblLnsBrandDesignType.ToDtoWithRelated(level - 1);
              target.TblLnsLensIndexMaterialTypes = source.TblLnsLensIndexMaterialTypes.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsDesignTypeLensIndex ToEntity(this TblLnsDesignTypeLensIndexDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsDesignTypeLensIndex();

            // Properties
            target.BrandDesignTypeId = source.BrandDesignTypeId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.LensIndexId = source.LensIndexId;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsDesignTypeLensIndexDto> ToDtos(this IEnumerable<TblLnsDesignTypeLensIndex> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsDesignTypeLensIndexDto> ToDtosWithRelated(this IEnumerable<TblLnsDesignTypeLensIndex> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsDesignTypeLensIndex> ToEntities(this IEnumerable<TblLnsDesignTypeLensIndexDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsDesignTypeLensIndex source, TblLnsDesignTypeLensIndexDto target);

        static partial void OnEntityCreating(TblLnsDesignTypeLensIndexDto source, TblLnsDesignTypeLensIndex target);

    }

}
