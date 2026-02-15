using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsCoatingConverter
    {

        public static TblLnsCoatingDto ToDto(this TblLnsCoating source)
        {
            return source.ToDtoWithRelated(0);
        }
	

		public static TblLnsCoatingDto ToDtoWithRelated(this TblLnsCoating source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsCoatingDto();

            // Properties
            target.CoatingId = source.CoatingId;
            target.Code = source.Code;
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
              target.TblLnsBrandCoatings = source.TblLnsBrandCoatings.ToDtosWithRelated(level - 1);
              target.TblLnsOrders = source.TblLnsOrders.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsCoating ToEntity(this TblLnsCoatingDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsCoating();

            // Properties
            target.CoatingId = source.CoatingId;
            target.Code = source.Code;
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

        public static List<TblLnsCoatingDto> ToDtos(this IEnumerable<TblLnsCoating> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsCoatingDto> ToDtos(this List<TblLnsCoating> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}
		public static List<TblLnsCoatingDto> ToDtosWithRelated(this IEnumerable<TblLnsCoating> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsCoatingDto> ToDtosWithRelated(this List<TblLnsCoating> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }

        public static List<TblLnsCoating> ToEntities(this IEnumerable<TblLnsCoatingDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsCoating source, TblLnsCoatingDto target);

        static partial void OnEntityCreating(TblLnsCoatingDto source, TblLnsCoating target);

    }

}
