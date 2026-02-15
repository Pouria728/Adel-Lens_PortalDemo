using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsDesignTypeConverter
    {

        public static TblLnsDesignTypeDto ToDto(this TblLnsDesignType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsDesignTypeDto ToDtoWithRelated(this TblLnsDesignType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsDesignTypeDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.LensTypeId = source.LensTypeId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.DesignTypeId = source.DesignTypeId;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.IsSpecial = source.IsSpecial;
            target.SphPlus = source.SphPlus;
            target.SphMinus = source.SphMinus;
            target.Addition = source.Addition;
            target.AdditionValue = source.AdditionValue;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrandDesignTypes = source.TblLnsBrandDesignTypes.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsDesignType ToEntity(this TblLnsDesignTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsDesignType();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.LensTypeId = source.LensTypeId;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.DesignTypeId = source.DesignTypeId;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.IsSpecial = source.IsSpecial;
            target.SphPlus = source.SphPlus;
            target.SphMinus = source.SphMinus;
            target.Addition = source.Addition;
            target.AdditionValue = source.AdditionValue;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsDesignTypeDto> ToDtos(this IEnumerable<TblLnsDesignType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }

        public static List<TblLnsDesignTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsDesignType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsDesignType> ToEntities(this IEnumerable<TblLnsDesignTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsDesignType source, TblLnsDesignTypeDto target);

        static partial void OnEntityCreating(TblLnsDesignTypeDto source, TblLnsDesignType target);

    }

}
