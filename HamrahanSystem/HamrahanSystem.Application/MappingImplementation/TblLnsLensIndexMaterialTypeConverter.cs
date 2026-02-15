using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensIndexMaterialTypeConverter
    {

        public static TblLnsLensIndexMaterialTypeDto ToDto(this TblLnsLensIndexMaterialType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensIndexMaterialTypeDto ToDtoWithRelated(this TblLnsLensIndexMaterialType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexMaterialTypeDto();

            // Properties
            target.DefineObjectId = source.DefineObjectId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.LensIndexMaterialTypeId = source.LensIndexMaterialTypeId;
            target.MaterialTypeId = source.MaterialTypeId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsDesignTypeLensIndex = source.TblLnsDesignTypeLensIndex.ToDtoWithRelated(level - 1);
              target.TblLnsMaterialType = source.TblLnsMaterialType.ToDtoWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensIndexMaterialType ToEntity(this TblLnsLensIndexMaterialTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexMaterialType();

            // Properties
            target.DefineObjectId = source.DefineObjectId;
            target.DesignTypeLensIndexId = source.DesignTypeLensIndexId;
            target.LensIndexMaterialTypeId = source.LensIndexMaterialTypeId;
            target.MaterialTypeId = source.MaterialTypeId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensIndexMaterialTypeDto> ToDtos(this IEnumerable<TblLnsLensIndexMaterialType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsLensIndexMaterialTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsLensIndexMaterialType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsLensIndexMaterialType> ToEntities(this IEnumerable<TblLnsLensIndexMaterialTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensIndexMaterialType source, TblLnsLensIndexMaterialTypeDto target);

        static partial void OnEntityCreating(TblLnsLensIndexMaterialTypeDto source, TblLnsLensIndexMaterialType target);

    }

}
