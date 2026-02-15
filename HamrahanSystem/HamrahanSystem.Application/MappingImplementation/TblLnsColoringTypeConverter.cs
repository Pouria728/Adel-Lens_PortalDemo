using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsColoringTypeConverter
    {

        public static TblLnsColoringTypeDto ToDto(this TblLnsColoringType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsColoringTypeDto ToDtoWithRelated(this TblLnsColoringType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsColoringTypeDto();

            // Properties
            target.Code = source.Code;
            target.ColoringTypeId = source.ColoringTypeId;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive); 
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsColoringType ToEntity(this TblLnsColoringTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsColoringType();

            // Properties
            target.Code = source.Code;
            target.ColoringTypeId = source.ColoringTypeId;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
			target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsColoringTypeDto> ToDtos(this IEnumerable<TblLnsColoringType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsColoringTypeDto> ToDtos(this List<TblLnsColoringType> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsColoringTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsColoringType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsColoringTypeDto> ToDtosWithRelated(this List<TblLnsColoringType> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsColoringType> ToEntities(this IEnumerable<TblLnsColoringTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsColoringType source, TblLnsColoringTypeDto target);

        static partial void OnEntityCreating(TblLnsColoringTypeDto source, TblLnsColoringType target);

    }

}
