using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsMaterialTypeConverter
    {

        public static TblLnsMaterialTypeDto ToDto(this TblLnsMaterialType source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsMaterialTypeDto ToDtoWithRelated(this TblLnsMaterialType source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsMaterialTypeDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive); 
            target.MaterialTypeId = source.MaterialTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsLensIndexMaterialTypes = source.TblLnsLensIndexMaterialTypes.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsMaterialType ToEntity(this TblLnsMaterialTypeDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsMaterialType();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.MaterialTypeId = source.MaterialTypeId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsMaterialTypeDto> ToDtos(this IEnumerable<TblLnsMaterialType> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsMaterialTypeDto> ToDtos(this List<TblLnsMaterialType> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsMaterialTypeDto> ToDtosWithRelated(this IEnumerable<TblLnsMaterialType> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsMaterialTypeDto> ToDtosWithRelated(this List<TblLnsMaterialType> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsMaterialType> ToEntities(this IEnumerable<TblLnsMaterialTypeDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblLnsMaterialType> ToEntities(this List<TblLnsMaterialTypeDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsMaterialType source, TblLnsMaterialTypeDto target);

        static partial void OnEntityCreating(TblLnsMaterialTypeDto source, TblLnsMaterialType target);

    }

}
