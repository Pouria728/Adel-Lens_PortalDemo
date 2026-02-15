using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensIndexConverter
    {

        public static TblLnsLensIndexDto ToDto(this TblLnsLensIndex source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensIndexDto ToDtoWithRelated(this TblLnsLensIndex source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.LensIndexId = source.LensIndexId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsDesignTypeLensIndices = source.TblLnsDesignTypeLensIndices.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensIndex ToEntity(this TblLnsLensIndexDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndex();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.LensIndexId = source.LensIndexId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensIndexDto> ToDtos(this IEnumerable<TblLnsLensIndex> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsLensIndexDto> ToDtos(this List<TblLnsLensIndex> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsLensIndexDto> ToDtosWithRelated(this IEnumerable<TblLnsLensIndex> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsLensIndexDto> ToDtosWithRelated(this List<TblLnsLensIndex> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}
		public static List<TblLnsLensIndex> ToEntities(this IEnumerable<TblLnsLensIndexDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}
		public static List<TblLnsLensIndex> ToEntities(this List<TblLnsLensIndexDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensIndex source, TblLnsLensIndexDto target);

        static partial void OnEntityCreating(TblLnsLensIndexDto source, TblLnsLensIndex target);

    }

}
