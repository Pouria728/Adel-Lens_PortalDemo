using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensIndexRConverter
    {

        public static TblLnsLensIndexRDto ToDto(this TblLnsLensIndexR source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensIndexRDto ToDtoWithRelated(this TblLnsLensIndexR source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexRDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.LensIndexRId = source.LensIndexRId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsLensTypeRLensIndexRs = source.TblLnsLensTypeRLensIndexRs.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensIndexR ToEntity(this TblLnsLensIndexRDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensIndexR();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.LensIndexRId = source.LensIndexRId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensIndexRDto> ToDtos(this IEnumerable<TblLnsLensIndexR> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsLensIndexRDto> ToDtos(this List<TblLnsLensIndexR> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsLensIndexRDto> ToDtosWithRelated(this IEnumerable<TblLnsLensIndexR> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsLensIndexRDto> ToDtosWithRelated(this List<TblLnsLensIndexR> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsLensIndexR> ToEntities(this IEnumerable<TblLnsLensIndexRDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }
		public static List<TblLnsLensIndexR> ToEntities(this List<TblLnsLensIndexRDto> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToEntity())
			  .ToList();

			return target;
		}

		static partial void OnDtoCreating(TblLnsLensIndexR source, TblLnsLensIndexRDto target);

        static partial void OnEntityCreating(TblLnsLensIndexRDto source, TblLnsLensIndexR target);

    }

}
