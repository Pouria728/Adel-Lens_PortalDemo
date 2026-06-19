using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsCustomLensTypeCoatingConverter
    {

        public static TblLnsCustomLensTypeCoatingDto ToDto(this TblLnsCustomLensTypeCoating source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsCustomLensTypeCoatingDto ToDtoWithRelated(this TblLnsCustomLensTypeCoating source, int level)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensTypeCoatingDto();

            // Properties
            target.CustomLensTypeCoatingId = source.CustomLensTypeCoatingId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensTypeName = source.LensTypeName;
            target.CoatingName = source.CoatingName;
            target.IsDefault = source.IsDefault == true;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToBoolean(source.IsActive);

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCustomLensTypeCoating ToEntity(this TblLnsCustomLensTypeCoatingDto source)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomLensTypeCoating();

            // Properties
            target.CustomLensTypeCoatingId = source.CustomLensTypeCoatingId;
            target.DesignTypeId = source.DesignTypeId;
            target.LensTypeName = source.LensTypeName;
            target.CoatingName = source.CoatingName;
            target.IsDefault = source.IsDefault;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToInt16(source.IsActive);

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsCustomLensTypeCoatingDto> ToDtos(this IEnumerable<TblLnsCustomLensTypeCoating> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeCoatingDto> ToDtos(this List<TblLnsCustomLensTypeCoating> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeCoatingDto> ToDtosWithRelated(this IEnumerable<TblLnsCustomLensTypeCoating> source, int level)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDtoWithRelated(level))
                .ToList();

            return target;
        }

        public static List<TblLnsCustomLensTypeCoating> ToEntities(this IEnumerable<TblLnsCustomLensTypeCoatingDto> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToEntity())
                .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsCustomLensTypeCoating source, TblLnsCustomLensTypeCoatingDto target);

        static partial void OnEntityCreating(TblLnsCustomLensTypeCoatingDto source, TblLnsCustomLensTypeCoating target);

    }

}

