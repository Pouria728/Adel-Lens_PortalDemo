using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Domain.Entity;

using System.Collections.Generic;
using System.Linq;

namespace HamrahanSystem.Application.MappingImplementation
{

    public static partial class TblLnsLensTypeRConverter
    {

        public static TblLnsLensTypeRDto ToDto(this TblLnsLensTypeR source)
        {
            return source.ToDtoWithRelated(0);
        }

        public static TblLnsLensTypeRDto ToDtoWithRelated(this TblLnsLensTypeR source, int level)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensTypeRDto();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToBoolean(source.IsActive);
            target.LensTypeRId = source.LensTypeRId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // Navigation Properties
            if (level > 0) {
              target.TblLnsBrandLensTypeRs = source.TblLnsBrandLensTypeRs.ToDtosWithRelated(level - 1);
            }

            // User-defined partial method
            OnDtoCreating(source, target);

            return target;
        }

        public static TblLnsLensTypeR ToEntity(this TblLnsLensTypeRDto source)
        {
            if (source == null)
              return null;

            var target = new TblLnsLensTypeR();

            // Properties
            target.Code = source.Code;
            target.Company = source.Company;
            target.CreateDate = source.CreateDate;
            target.CreatedBy = source.CreatedBy;
            target.Description = source.Description;
            target.IsActive = Convert.ToInt16(source.IsActive);
            target.LensTypeRId = source.LensTypeRId;
            target.ModifiedBy = source.ModifiedBy;
            target.ModifiedDate = source.ModifiedDate;
            target.Name = source.Name;
            target.OrderId = source.OrderId;

            // User-defined partial method
            OnEntityCreating(source, target);

            return target;
        }

        public static List<TblLnsLensTypeRDto> ToDtos(this IEnumerable<TblLnsLensTypeR> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDto())
              .ToList();

            return target;
        }
		public static List<TblLnsLensTypeRDto> ToDtos(this List<TblLnsLensTypeR> source)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDto())
			  .ToList();

			return target;
		}

		public static List<TblLnsLensTypeRDto> ToDtosWithRelated(this IEnumerable<TblLnsLensTypeR> source, int level)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToDtoWithRelated(level))
              .ToList();

            return target;
        }
		public static List<TblLnsLensTypeRDto> ToDtosWithRelated(this List<TblLnsLensTypeR> source, int level)
		{
			if (source == null)
				return null;

			var target = source
			  .Select(src => src.ToDtoWithRelated(level))
			  .ToList();

			return target;
		}

		public static List<TblLnsLensTypeR> ToEntities(this IEnumerable<TblLnsLensTypeRDto> source)
        {
            if (source == null)
              return null;

            var target = source
              .Select(src => src.ToEntity())
              .ToList();

            return target;
        }

        static partial void OnDtoCreating(TblLnsLensTypeR source, TblLnsLensTypeRDto target);

        static partial void OnEntityCreating(TblLnsLensTypeRDto source, TblLnsLensTypeR target);

    }

}
