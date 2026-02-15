using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsCustomLensIndexConverter
    {

        public static TblLnsCustomLensIndexDto ToDto(this TblLnsCustomLensIndex source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsCustomLensIndexDto ToDtoWithRelated(this TblLnsCustomLensIndex source, int level)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensIndexDto();

            // Properties
            target.CustomLensIndexId = source.CustomLensIndexId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensIndexName = source.LensIndexName;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToBoolean(source.IsActive);
            target.HasColoringType = string.Equals(source.ColoringTypeStatus, "دارد", System.StringComparison.OrdinalIgnoreCase);

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCustomLensIndex ToEntity(this TblLnsCustomLensIndexDto source)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensIndex();

            // Properties
            target.CustomLensIndexId = source.CustomLensIndexId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensIndexName = source.LensIndexName;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToInt16(source.IsActive);
            target.ColoringTypeStatus = source.HasColoringType ? "دارد" : "ندارد";

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsCustomLensIndexDto> ToDtos(this IEnumerable<TblLnsCustomLensIndex> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensIndexDto> ToDtos(this List<TblLnsCustomLensIndex> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensIndexDto> ToDtosWithRelated(this IEnumerable<TblLnsCustomLensIndex> source, int level)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDtoWithRelated(level))
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensIndex> ToEntities(this IEnumerable<TblLnsCustomLensIndexDto> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToEntity())
                .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsCustomLensIndex source, TblLnsCustomLensIndexDto target);

        static partial void OnEntityCreating(TblLnsCustomLensIndexDto source, TblLnsCustomLensIndex target);

    }

}
