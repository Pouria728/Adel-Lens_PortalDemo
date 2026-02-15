
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{
    public static partial class TblLnsCustomDesignTypeAdditionConverter
    {
        public static TblLnsCustomDesignTypeAdditionDto ToDto(this TblLnsCustomDesignTypeAddition source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsCustomDesignTypeAdditionDto ToDtoWithRelated(this TblLnsCustomDesignTypeAddition source, int level)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomDesignTypeAdditionDto();

            // Properties
            target.CustomDesignTypeAdditionId = source.CustomDesignTypeAdditionId;
            target.DesignTypeId = source.DesignTypeId;
            target.DefineObjectId = source.DefineObjectId;
            target.AdditionValue = source.AdditionValue;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToBoolean(source.IsActive);

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCustomDesignTypeAddition ToEntity(this TblLnsCustomDesignTypeAdditionDto source)
        {
            if (source == null)
                return null;

            var target = new TblLnsCustomDesignTypeAddition();

            // Properties
            target.CustomDesignTypeAdditionId = source.CustomDesignTypeAdditionId;
            target.DesignTypeId = source.DesignTypeId;
            target.DefineObjectId = source.DefineObjectId;
            target.AdditionValue = source.AdditionValue;
            target.OrderId = source.OrderId;
            target.IsActive = System.Convert.ToInt16(source.IsActive);

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsCustomDesignTypeAdditionDto> ToDtos(this IEnumerable<TblLnsCustomDesignTypeAddition> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomDesignTypeAdditionDto> ToDtos(this List<TblLnsCustomDesignTypeAddition> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDto())
                .ToList();

            return target;
        }

        public static List<TblLnsCustomDesignTypeAdditionDto> ToDtosWithRelated(this IEnumerable<TblLnsCustomDesignTypeAddition> source, int level)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToDtoWithRelated(level))
                .ToList();

            return target;
        }

        public static List<TblLnsCustomDesignTypeAddition> ToEntities(this IEnumerable<TblLnsCustomDesignTypeAdditionDto> source)
        {
            if (source == null)
                return null;

            var target = source
                .Select(src => src.ToEntity())
                .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsCustomDesignTypeAddition source, TblLnsCustomDesignTypeAdditionDto target);

        static partial void OnEntityCreating(TblLnsCustomDesignTypeAdditionDto source, TblLnsCustomDesignTypeAddition target);
    }
}
