
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
    public static partial class TblLnsCustomLensTypeMaterialConverter
    {
        public static TblLnsCustomLensTypeMaterialDto ToDto(this TblLnsCustomLensTypeMaterial source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsCustomLensTypeMaterialDto ToDtoWithRelated(this TblLnsCustomLensTypeMaterial source, int level)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensTypeMaterialDto();

            // Properties
            target.CustomLensTypeMaterialId = source.CustomLensTypeMaterialId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensIndexId = source.LensIndexId;
            target.LensTypeName = source.LensTypeName;
            target.MaterialName = source.MaterialName;
            target.DefineObjectId = source.DefineObjectId;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToBoolean(source.IsActive);

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCustomLensTypeMaterial ToEntity(this TblLnsCustomLensTypeMaterialDto source)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensTypeMaterial();

            // Properties
            target.CustomLensTypeMaterialId = source.CustomLensTypeMaterialId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensIndexId = source.LensIndexId;
            target.LensTypeName = source.LensTypeName;
            target.MaterialName = source.MaterialName;
            target.DefineObjectId = source.DefineObjectId;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToInt16(source.IsActive);

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsCustomLensTypeMaterialDto> ToDtos(this IEnumerable<TblLnsCustomLensTypeMaterial> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeMaterialDto> ToDtos(this List<TblLnsCustomLensTypeMaterial> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeMaterialDto> ToDtosWithRelated(this IEnumerable<TblLnsCustomLensTypeMaterial> source, int level)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDtoWithRelated(level))
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeMaterial> ToEntities(this IEnumerable<TblLnsCustomLensTypeMaterialDto> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToEntity())
                .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsCustomLensTypeMaterial source, TblLnsCustomLensTypeMaterialDto target);

        static partial void OnEntityCreating(TblLnsCustomLensTypeMaterialDto source, TblLnsCustomLensTypeMaterial target);
    }
}
